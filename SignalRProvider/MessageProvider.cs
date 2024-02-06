using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRProvider
{
	public class MessageProvider : Core.Disposable
	{
		public bool NewMessage(Entities.Message ms)
			{
			if ( ms.messageImage == null)
			{
				ms.messageImage = "";
			}
			if (ms.messageContent == null)
			{
				ms.messageContent = "";
			}
			using var DAL = new DataAccess.DataAccessLayer();
			DAL.Parameters = new List<SqlParameter> {

				new SqlParameter{ ParameterName = "@messageSenderId", Value =  ms.messageSenderId},
				new SqlParameter{ ParameterName = "@messageRecieverId", Value = ms.messageRecieverId },
				new SqlParameter{ ParameterName = "@messageDateTime", Value =  ms.messageDateTime },
				new SqlParameter{ ParameterName = "@messageContent", Value =  ms.messageContent },
				new SqlParameter{ ParameterName = "@messageImage", Value =  ms.messageImage },
				new SqlParameter{ ParameterName = "@messageStatus", Value =  ms.messageStatus },
				new SqlParameter{ ParameterName = "@messageType", Value =  ms.messageType },
				new SqlParameter{ ParameterName = "@messagePathIfExist", Value =  ms.messagePathIfExist },
			};
			return DAL.ExecuteNonQuery("spNewMessage");
		}
		
		public bool userOpenedChat(int accountId, int friendId)
		{
			using var DAL = new DataAccess.DataAccessLayer();
			DAL.Parameters = new List<SqlParameter> {
				new SqlParameter{ ParameterName = "@messageSenderId", Value = accountId},
				new SqlParameter{ ParameterName = "@messageRecieverId", Value = friendId},
			};
			return DAL.ExecuteNonQuery("spUserOpenedChat");
		}
		public List<Entities.Message> ListAllChatMessage(int accountId, int friendId)
		{

			using var DAL2 = new DataAccess.DataAccessLayer();
			DAL2.Parameters = new List<SqlParameter> {
				new SqlParameter{ ParameterName = "@messageSenderId", Value =  accountId},
				new SqlParameter{ ParameterName = "@messageRecieverId", Value = friendId },
			};
			return DAL2.ExecuteReader<Entities.Message>("spListAllChatMessage");
		}
		public Message GetLastMessage(int senderId, int recieverId)
		{

			using var DAL2 = new DataAccess.DataAccessLayer();
			DAL2.Parameters = new List<SqlParameter> {
				new SqlParameter{ ParameterName = "@senderId", Value = senderId },
				new SqlParameter{ ParameterName = "@recieverId", Value = recieverId },
			};
			return DAL2.ExecuteReader<Message>("spGetLastMessage").FirstOrDefault();
		}
	}
}
