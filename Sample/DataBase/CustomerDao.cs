using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Sample.DataBase;
using System.Data;
using System.Configuration;

namespace Sample.DataBase
{
    public class CustomerDao
    {
        private SqlConnection connection;
        private DBManager dbManager;

        const string DB_NAME = "Sample";

        public CustomerDao()
        {
            dbManager = new DBManager();
            connection = dbManager.GetConnection(DB_NAME);
        }

        /// <summary>
        /// 会員情報を取得します。（ページング用）
        /// </summary>
        /// <param name="from">取得先頭行数</param>
        /// <param name="to">取得対象末行数</param>
        /// <param name="filterString">WHERE句文字列</param>
        /// <param name="sortString">ORDER句文字列</param>
        /// <returns></returns>
        public DataTable Select(int from, int to, String filterString, String sortString)
        {
            //データアダプタ
            SqlDataAdapter da = new SqlDataAdapter();

            //SQL
            string strSQL = "SELECT * FROM ";
            strSQL += "(SELECT *,ROW_NUMBER() OVER (ORDER BY " + sortString + ") AS RowNo FROM tbSampleCustomer ";
            if (!String.IsNullOrEmpty(filterString))
            {
                strSQL += "WHERE " + filterString;
            }
            strSQL += ") as rowTable ";
            strSQL += "WHERE rowTable.RowNo BETWEEN " + from.ToString() + " AND " + to.ToString();
            strSQL += " ORDER BY " + sortString + ";";

            da.SelectCommand = new System.Data.SqlClient.SqlCommand(strSQL, this.connection);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandTimeout = this.dbManager.GetTimeOut();

            DataSet ds = new DataSet();
            da.Fill(ds, "Customer");

            return ds.Tables["Customer"].Copy();
        }

        /// <summary>
        /// 会員情報の件数を取得します。（ページング用）
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public int GetRecordCounts(String filterString)
        {
            SqlDataAdapter da = new SqlDataAdapter();

            string strSQL = "SELECT count(ID) As count FROM tbSampleCustomer";
            if (!String.IsNullOrEmpty(filterString))
            {
                strSQL += " WHERE " + filterString;
            }

            da.SelectCommand = new System.Data.SqlClient.SqlCommand(strSQL, this.connection);
            da.SelectCommand.CommandType = CommandType.Text;
            da.SelectCommand.CommandTimeout = this.dbManager.GetTimeOut();

            DataSet ds = new DataSet();
            da.Fill(ds, "CustomerCount");

            return (int)ds.Tables["CustomerCount"].Rows[0]["count"];
        }


    }
}