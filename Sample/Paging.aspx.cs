using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sample.DataBase;
using System.Data;
using Sample.DataBase;

namespace Sample
{
    public partial class Paging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Customer.Count"] = 0;
                Session["Customer.Filter"] = "";
            }
        }

        //ページロード後に件数表示
        protected void Page_SaveStateComplete(object sender, EventArgs e)
        {
            labCount.Text = Session["Customer.Count"].ToString() + "件";
        }

        //総件数を取得します。（ページング用）
        public int GetCount(int startRowIndex, int maximumRows)
        {
            string filter = GetFilterString();

            CustomerDao dao = new CustomerDao();
            int counts = dao.GetRecordCounts(filter);
            Session["Customer.Count"] = counts.ToString();
            return counts;
        }

        //会員情報を取得します。（ページング用）
        public DataSet GetCustomer(int startRowIndex, int maximumRows)
        {
            string Filter = GetFilterString();

            CustomerDao dao = new CustomerDao();
            DataSet ds = new DataSet();
            ds.Tables.Add(dao.Select(startRowIndex + 1, startRowIndex + 1 + maximumRows, GetFilterString(), "ID asc"));

            return ds;
        }

        //フィルタ文字列を作成
        private void SetFilterString()
        {
            //エラーメッセージクリア
            labErrID.Text = "";

            string filterString = "";

            //Type
            string type = ddlType.SelectedValue.ToString();
            if (!type.Equals("絞込みなし"))
            {
                filterString += "Type = '" + type + "'";
            }
            
            //ID（開始）
            if (!String.IsNullOrEmpty(txtIDFrom.Text))
            {
                try
                {
                    int fromID = int.Parse(txtIDFrom.Text);
                    if (String.IsNullOrEmpty(filterString))
                    {
                        filterString += "ID >= " + fromID.ToString();
                    }
                    else
                    {
                        filterString += " AND ID >= " + fromID.ToString();
                    }
                }
                catch
                {
                    labErrID.Text = "<BR>IDは数値で指定して下さい。";
                }
            }

            //ID（終了）
            if (!String.IsNullOrEmpty(txtIDTo.Text))
            {
                try
                {
                    int toID = int.Parse(txtIDTo.Text);
                    if (String.IsNullOrEmpty(filterString))
                    {
                        filterString += "ID <= " + toID.ToString();
                    }
                    else
                    {
                        filterString += " AND ID <= " + toID.ToString();
                    }
                }
                catch
                {
                    labErrID.Text = "<BR>IDは数値で指定して下さい。";
                }
            }

            Session["Customer.Filter"] = filterString;
        }

        //フィルタ文字列を取得します。
        private string GetFilterString()
        {
            string filter = "";
            try
            { filter = Session["Customer.Filter"].ToString(); }
            catch
            { }

            return filter;
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFilterString();
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        protected void txtIDFrom_TextChanged(object sender, EventArgs e)
        {
            SetFilterString();
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }

        protected void txtIDTo_TextChanged(object sender, EventArgs e)
        {
            SetFilterString();
            GridView1.PageIndex = 0;
            GridView1.DataBind();
        }
    }
}