using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperObjector
{
    public static class DapperObjector
    {

        public static int Insert(this IDbConnection db, object model)
        {

            var properyAndNames = model.ExposePropertyNames(true);
            var namesWithSign = AddSignToProperyNames(properyAndNames);
            var fieldsToInsert = string.Join(",", properyAndNames);
            var fieldsParamsToInsert = string.Join(",", namesWithSign);

            var tableName = model.ExposeObjectName();
            string sqlQuery = $"Insert into {tableName} ({fieldsToInsert}) Values ({fieldsParamsToInsert})";

            return db.Execute(sqlQuery, model);

        }



        public static int Update(this IDbConnection db, object model, string whereStatament = "")
        {
            var properyAndNames = model.ExposePropertyNames(true);
            var fieldsToUpdate = DecorateUpdateFields(properyAndNames);


            if (string.IsNullOrEmpty(whereStatament))
            {
                var keyObject = model.GetObjectKey();

                if (!keyObject.Equals(default(KeyValuePair<string, object>)))
                    whereStatament = $"{keyObject.Key}={keyObject.Value}";
                else
                    throw new Exception("Key is not defined");

            }


            var tableName = model.ExposeObjectName();
            string sqlQuery = $"Update {tableName} set {fieldsToUpdate}  where {whereStatament}";
            return db.Execute(sqlQuery, model);
        }



        private static string[] AddSignToProperyNames(string[] propNames)
        {
            var propperties = new string[propNames.Length];
            Array.Copy(propNames, propperties, propNames.Length);


            for (int i = 0; i < propperties.Length; i++)
            {
                propperties[i] = $"@{propperties[i]}";
            }
            return propperties;
        }

        private static string DecorateUpdateFields(string[] propertyNames)
        {


            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                sb.Append($"{propertyNames[i]}=@{propertyNames[i]}");
                if (i != propertyNames.Length - 1)
                    sb.Append(",");

            }
            return sb.ToString();
        }



    }
}
