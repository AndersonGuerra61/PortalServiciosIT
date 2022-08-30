using System;
using System.Data;
using System.Data.SqlClient;
using DXWebApplication.App_Code.Utilidades;

namespace DXWebApplication.App_Code.Dal
{
    public class ClsConexion
    {
        
        string strConexion = System.Configuration.ConfigurationManager.ConnectionStrings["SrvSistema"].ConnectionString;       
        
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter dt = new SqlDataAdapter();
        DataSet ds;

        ClsErrorHandler error = new ClsErrorHandler();

        
        //Metodo que ejecuta un instrucción SQL y retorna un DATASET
        public DataSet EjectuaSQL(string strSql, string tabla)
        {
            SqlConnection conexion = null;
            try
            {
                conexion = OpenConexion();
                dt = new SqlDataAdapter(strSql, conexion);
                ds = new DataSet(tabla);
                dt.Fill(ds, tabla);

            }catch(Exception ex){

                error.LogError(ex.ToString(), ex.StackTrace);
            }
            finally
            {
                CloseConexion(conexion);
            }

            return ds;
        }              

        //Metodo para formatear una instrucción SQL cuando es SP
        public static SqlCommand getCommnad(string dbQuery, SqlConnection con, bool esStoreProc = false)
        {
            dynamic cmd = new SqlCommand(dbQuery, con);
            if (esStoreProc)
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }
            return cmd;
        }

        //Metodo que ejecuta una sentencia SQL retornando un valor booleano 
        public bool ejecutarNonQuery(string dbQuery, bool esStoreProc = false)
        {

            bool respuesta = false;

            if (dbQuery.Trim().Length == 0)
            {
                return false;
            }

            SqlConnection con = OpenConexion();
            using (SqlCommand cmd = getCommnad(dbQuery, con, esStoreProc))
            {
                try
                {
                    
                    cmd.ExecuteNonQuery();
                    respuesta = true;
                }
                catch (Exception ex)
                {
                    error.LogError(ex.ToString(), ex.StackTrace);
                    respuesta = false;
                }
                finally
                {

                    con = CloseConexion(con);
                }
            }

            return respuesta;
        }

        //Metodo que ejecuta un SP retornando un Dataset para procesos transaccionales
        public DataSet EjectSP_ReturnValueT(SqlConnection con, SqlTransaction trans, string strSql, string tabla)
        {
          
                SqlCommand sqlCommand = new SqlCommand(strSql, con, trans);
                dt.SelectCommand = sqlCommand;
                ds = new DataSet(tabla);
                dt.Fill(ds, tabla);            
            

            return ds;
        }      
        
        //Metodo que ejecuta sentencia SQL N numero de veces en procesos transaccionales
        public void EjectuaSQLT(SqlConnection con, SqlTransaction trans, String strSql)
        {       
                new SqlCommand(strSql, con, trans).ExecuteNonQuery();            
        }

        //Metodo que abre una conexión hacia la BD
        public SqlConnection OpenConexion()
        {
            SqlConnection con = new SqlConnection(strConexion);
            try
            {
                 
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);

            }

            return con;
        }

        //Metodo que cierra una conexion hacia la BD
        public SqlConnection CloseConexion(SqlConnection con)
        {
            
            try
            {
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                } 

            }
            catch (Exception ex)
            {
                error.LogError(ex.ToString(), ex.StackTrace);
            }

            return con;
        }


    }
}