using Microsoft.Data.SqlClient;

namespace SignalRProvider
{
    public class AccountProvider : Core.Disposable
    {

        public bool spSignUpAccount(Entities.Account account)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@FullName", Value =  account.FullName },
                new SqlParameter{ ParameterName = "@UserName", Value =  account.UserName },
                new SqlParameter{ ParameterName = "@Email", Value =  account.Email },
                new SqlParameter{ ParameterName = "@Password", Value =  account.Password },
                new SqlParameter{ ParameterName = "@PhoneNumber", Value =  account.PhoneNumber },
            };
            return DAL.ExecuteNonQuery("spSignUpAccount");
        }
        

        public Entities.Account spSignInRequest(Entities.Account account)
        {
         
            using var DAL2 = new DataAccess.DataAccessLayer();
            DAL2.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@UserName", Value =  account.UserName },
                new SqlParameter{ ParameterName = "@Password", Value = account.Password },
            };
            return DAL2.ExecuteReader<Entities.Account>("spSignInRequest").FirstOrDefault();
        }
        
        public Entities.Account returnAccountOnId(int Id)
        {
            using var DAL2 = new DataAccess.DataAccessLayer();
            DAL2.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  Id},
            };
            return DAL2.ExecuteReader<Entities.Account>("spreturnAccountOnId").FirstOrDefault();
        }

        
        public bool spStartConnection(int Id , string ConnectionId)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  Id },
                new SqlParameter{ ParameterName = "@ConnectionId", Value =  ConnectionId },
               
            };
            return DAL.ExecuteNonQuery("spStartConnection");
        }
        public bool spEndConnection(int Id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  Id },

            };
            return DAL.ExecuteNonQuery("spEndConnection");
        }


        public List<Entities.Account> spListAllAccounts()
        {

            using var DAL2 = new DataAccess.DataAccessLayer();
            DAL2.Parameters = new List<SqlParameter> {};
            return DAL2.ExecuteReader<Entities.Account>("spListAllOnlineAccounts");
        }

        public bool UpdateLastActivity(int Id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  Id },

            };
            return DAL.ExecuteNonQuery("UpdateLastActivity");
        }
        
        
        public bool ConnectUser(string conId, int userId)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@conId", Value =  conId },
                new SqlParameter{ ParameterName = "@userId", Value =  userId },
			};
            return DAL.ExecuteNonQuery("spConnectUser");
        }
        

















        /*
        public List<Entities.Contact> getAllContacts()
        {
            using var DAL = new DataAccess.DataAccessLayer();
            return DAL.ExecuteReader<Entities.Contact>("spSelectFromTblContact");
        } 
        public bool deleteContact(int Id)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Id", Value =  Id }
            };
            return DAL.ExecuteNonQuery("spDelFromTblContact");
        }
        public bool newContactRequest(Entities.Contact contact)
        {
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@Name", Value =  contact.Name },
                new SqlParameter{ ParameterName = "@Email", Value =  contact.Email },
                new SqlParameter{ ParameterName = "@PhoneNumber", Value =  contact.PhoneNumber },
                new SqlParameter{ ParameterName = "@Service", Value =  contact.Service },
                new SqlParameter{ ParameterName = "@message", Value =  contact.message },

            };
            return DAL.ExecuteNonQuery("spInsertIntoTblContact");
        }
        public List<Entities.Contact> SearchIntblContact(string searchString, int start, int end)
        {
            string newStr = '%' + searchString + '%';
            using var DAL2 = new DataAccess.DataAccessLayer();
            DAL2.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@searchString", Value =  newStr },
                new SqlParameter{ ParameterName = "@end", Value = end },
                new SqlParameter{ ParameterName = "@start", Value = start },
            };
            return DAL2.ExecuteReader<Entities.Contact>("spSearchIntblContactbyName");
        }
        public int spCountSearchByName(string searchString)
        {
            string newStr = '%' + searchString + '%';
            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@searchedName", Value =  newStr },
            };
            var Count = DAL.ExecuteReader<ViewModel.AdminViewModels.CountContactsViewModel>("spCountSearchByName").FirstOrDefault();

            return Count.Count;

        }
        ////////////////////////////////////////////////////////////////////
        public List<Entities.Contact> spNewSearchIntblContact(string searchString, string filter, int start, int end)
        {
            filter = "%" + filter + "%";
            if (searchString == null)
            {
                searchString = "%%";
            }
            if (filter == "%null%")
            {
                filter = "%%";
            }


            string newStr = '%' + searchString + '%';
            if (filter == null || filter == "null")
            {
                filter = "%%";
            }
            using var DAL2 = new DataAccess.DataAccessLayer();
            DAL2.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@searchString", Value =  newStr },
                new SqlParameter{ ParameterName = "@filterValue", Value =  filter },
                new SqlParameter{ ParameterName = "@end", Value = end },
                new SqlParameter{ ParameterName = "@start", Value = start },
            };
            return DAL2.ExecuteReader<Entities.Contact>("SearchAndFilter");
        }

        public int spNewCountSearchByName(string searchString, string filter)
        {

            using var DAL = new DataAccess.DataAccessLayer();
            DAL.Parameters = new List<SqlParameter> {
                new SqlParameter{ ParameterName = "@searchString", Value =  searchString },
                new SqlParameter{ ParameterName = "@filterValue", Value =  filter },

            };
            var Count = DAL.ExecuteReader<ViewModel.AdminViewModels.CountContactsViewModel>("SearchAndFilterCount").FirstOrDefault();

            return Count.Count;

        }
        */

    }
}

