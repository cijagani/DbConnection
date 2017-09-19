using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Script.Serialization;

public class dbconnection
{
    #region  variables

    //private member _con of sqlconnection class
    private static SqlConnection _conn;

    //private member _da of sqlDataAdapter class
    private static SqlDataAdapter _da;

    //private member _cmd of sqlCommand class
    private static SqlCommand _cmd;

    //private member _ds of DataSet class
    private static DataSet _ds;

    //private member _dt of DataTable class
    private static DataTable _dt;

    #endregion

    #region Constructor

    public dbconnection(string ConnectionStringName)
    {
        _da = new SqlDataAdapter();
        _cmd = new SqlCommand();
        _dt = new DataTable();
        _ds = new DataSet();
        _conn = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString);
    }

    #endregion

    #region method for open connection
    //private method for open the connection and return connection object
    private static SqlConnection openconnection()
    {
        //check connection state if closed or broken then open connection otherwise not.
        try
        {
            if (_conn.State == ConnectionState.Closed ||
                _conn.State == ConnectionState.Broken)
                _conn.Open();
        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            //return null;
        }
        return _conn;
    }

    #endregion

    #region method for close connection
    //private method for close the connection and return connection object
    private static SqlConnection closeconnection()
    {
        //check connection state if open or broken then close connection otherwise not.
        try
        {
            if (_conn.State == ConnectionState.Open || _conn.State == ConnectionState.Broken)
                _conn.Close();
        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            //return null;
        }
        return _conn;
    }
    #endregion

    #region method for Insert Data
    //public method for insert
    public static bool ExecuteInsert(String _insertQuery)
    {
        try
        {
            _cmd.Connection = openconnection();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = _insertQuery;
            _da.InsertCommand = _cmd;
            _cmd.ExecuteNonQuery();
            //_conn.Close();
            closeconnection();
        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            //return false;
        }
        finally
        {
            _conn.Close();
        }
        return true;
    }
    #endregion

    #region method for Update Data
    //public method for Update
    public static bool ExecuteUpdate(string _updateQuery)
    {

        try
        {
            _cmd.Connection = openconnection();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = _updateQuery;
            _da.UpdateCommand = _cmd;
            _cmd.ExecuteNonQuery();
        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            // return false;
        }
        return true;
    }
    #endregion

    #region method for Delete Data
    //public method for delete
    public static bool ExecuteDelete(string _deleteQuery)
    {
        try
        {
            _cmd.Connection = openconnection();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = _deleteQuery;
            _da.DeleteCommand = _cmd;
            _cmd.ExecuteNonQuery();
        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            //return false;
        }
        finally
        {
            if (_conn != null) _conn.Close();
        }
        return true;

    }
    #endregion

    #region method for Select Data
    //public method for select
    public static DataTable ExecuteSelect(string _selectQuery)
    {
        try
        {
            _cmd.Connection = openconnection();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = _selectQuery;
            _cmd.ExecuteNonQuery();
            _da.SelectCommand = _cmd;
            _da.Fill(_ds);
            _dt = _ds.Tables[0];


        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            //return null;
        }
       
        return _dt;
    }
    #endregion

    #region method for SelectCheck Data
    //public method for selectCheck
    public static Boolean ExecuteSelectCheck(string _selectQuery)
    {
        bool flag = false;
        try
        {
            _cmd.Connection = openconnection();
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = _selectQuery;
            _da.SelectCommand = _cmd;
            if (Convert.ToInt32(_cmd.ExecuteScalar()) == 1)
            {
                return flag = true;
            }
        }
        catch (SqlException SqlEx)
        {
            throw new Exception(SqlEx.Message + "\n" + SqlEx.Number);
            //return null;
        }
        return flag;

    }
    #endregion

    #region method for Covert DataTable to JSON
    public static string GET_JSON(DataTable dt)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<object> rows = new List<object>();
        List<object> row=new List<object>();
        foreach (DataRow dr in dt.Rows)
        {
            foreach (DataColumn col in dt.Columns)
            {
                rows.Add(dr[col]);
            }
        }
        return serializer.Serialize(rows);

    }
    #endregion
	
	public static string PRITI_ZALORIYA(){
		
	}
}
