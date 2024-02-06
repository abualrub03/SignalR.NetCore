console.log("testhub");
var image;

function setInputMessageAsDefault() {
    document.getElementById("iamgesContainer").innerHTML = ``;
    document.getElementById("content").style.marginBottom = "0px";
    document.getElementById("bottom").style.marginTop = "0px";
    image = null;
    file = null;
    $("#msgContainer")[0].scrollIntoView(false);
    const fileInput = document.getElementById("imageUpload");
    const dataTransfer = new DataTransfer();
    dataTransfer.items.add(new File([""], "new_file.jpg"));
    fileInput.files = dataTransfer.files;
}
function sentImageClickButton() {
    var formData = new FormData();
    formData.append('image', image);
    formData.append('senderId', '@Model.account.Id');
    formData.append('recieverId', '@Model.accounts[0].Id');
    formData.append('textMessage', document.getElementById("messageInput").value);
    $.ajax({
        url: '/Home/UploadImage',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (path) {
            sentImage(path);
            connection.invoke("SendToUser", @Model.account.Id, @Model.accounts[0].Id, path[0], path[1], 'image')
                .catch(function (err) {
                    console.error(err.toString());
                });
            setInputMessageAsDefault();
        },
        error: function (error) { }
    });
}

function removeImage() { }
$(function () {
    $("#msgContainer")[0].scrollIntoView(false)
});
$("#messageInput").keydown(function (event) {
    if (event.key === "Enter") {
        if (image == null) {
            var message = document.getElementById("messageInput").value;
            connection.invoke("SendToUser", @Model.account.Id, @Model.accounts[0].Id, '', message, 'text').catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
            sentMessage(message);
            document.getElementById("messageInput").value = '';
            $("#msgContainer")[0].scrollIntoView(false)
        } else {
            console.log("yupwefuckedup");
            sentImageClickButton();
            image = null;
        }
    }
});
setInterval(function () {
    $.ajax({
        url: "/Home/CheckActivity",
        method: "POST",
        data: {
            Id: "@Model.accounts[0].Id",
        },
        success: function (data) {
            if (data == 1) {
                document.getElementById("statusChat").innerHTML =
                    `<i class="material-icons online">fiber_manual_record</i>`;
                document.getElementById("ActiveInActive").innerHTML =
                    `<span>Active Now</span>`;
            } else {
                document.getElementById("statusChat").innerHTML =
                    `<i class="material-icons offline">fiber_manual_record</i>`;
                document.getElementById("ActiveInActive").innerHTML =
                    `<span>Inactive</span>`;
            }
        },
        error: function (error) {
            consoe.log("error");
        }
    });
}, 0.1 * 60 * 1000);

function recevedMessage(user, message) {
    if (@Model.accounts[0].Id == user) {
        document.getElementById("msgContainer").innerHTML +=
            `<div class="message">
					<img class="avatar-md" src="/img/avatars/avatar-female-5.jpg" data-toggle="tooltip" data-placement="top" title="Keith" alt="avatar">
					<div class="text-main">
						<div class="text-group">
							<div class="text">
								<p> ` + message + ` </p>
							</div>
						</div>
					</div>
				</div>`;
        $("#msgContainer")[0].scrollIntoView(false)
    }
}

function sentMessage(message) {
    document.getElementById("msgContainer").innerHTML +=
        `<div class="message me">
				<div class="text-main">
					<div class="text-group me">
						<div class="text me">
							<p> ` + message + ` </p>
						</div>
					</div>
				</div>
			</div>`;
    $("#msgContainer")[0].scrollIntoView(false)
}

function recevedImage(user, imageTextMessage, textMessage) {
    if (@Model.accounts[0].Id == user) {
        document.getElementById("msgContainer").innerHTML +=
            `<div class="message">
					<img class="avatar-md" src="/img/avatars/avatar-female-5.jpg" data-toggle="tooltip" data-placement="top" title="Keith" alt="avatar">
					<div class="text-main">
						<div class="text-group">
							<div class="text">
								<div class="chat-message chat-message-image">
									<img src="` + textMessage + `" style="width: 300px" alt="Image description">
								</div>
								<p> ` + imageTextMessage + ` </p>
							</div>
						</div>
					</div>
				</div>`;
        $("#msgContainer")[0].scrollIntoView(consoe)
    }
}

function sentImage(path) {
    document.getElementById("msgContainer").innerHTML +=
        `<div class="message me">
				<div class="text-main">
					<div class="text-group me">
						<div class="text me">
							<div class="chat-message chat-message-image">
								<img src="` + path[1] + `" style="width:300px" alt="Image description">
							</div>
								<p>` + path[0] + ` </p>
						</div>
					</div>
				</div>
			</div>`;
    $("#msgContainer")[0].scrollIntoView(false)
}
document.getElementById("sendToUser").addEventListener("click", function (event) {
    if (image == null) {
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendToUser", @Model.account.Id, @Model.accounts[0].Id, '', message, 'text').catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
        sentMessage(message);
        document.getElementById("messageInput").value = '';
        $("#msgContainer")[0].scrollIntoView(false)
    } else {
        console.log("yupwefuckedup");
        sentImageClickButton();
        image = null;
    }
});
/////////////////////////////////////////////////////////////////////////////////
$('#imageUpload').change(function () {
    console.log("fffffffffffffffk");
    var file = this.files[0];
    image = this.files[0];
    // Preview the image using FileReader
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById("iamgesContainer").innerHTML =
                `
						<img id="uploadedImage" style="width: 100px; height: 100px; margin-right: 30px;" src="` + e.target.result + `" id="impPrev" />
						<img src="/img/close.png" id="close"   style="position: absolute; top: 5px; right: 35px; width:20px;" onclick="setInputMessageAsDefault()" />
					`;

            document.getElementById("content").style.marginBottom = "-70px";
            document.getElementById("bottom").style.marginTop = "30px";
        };
        reader.readAsDataURL(file);
    }
});