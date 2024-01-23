using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public class Message
	{
		public int Id 					 {set;get;}
		public int messageSenderId 		  {set;get;}
		public int messageRecieverId 	  {set;get;}
		public DateTime messageDateTime  {set;get;}
		public string messageContent 	  {set;get;}
		public string messageStatus { set; get; }
	}
}
