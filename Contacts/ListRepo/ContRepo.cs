using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Contacts.Models;

namespace Contacts.ListRepo
{
    public class ContRepo
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Employee details    
        public bool AddContact(ContModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewContact", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@firstName", obj.FirstName);
            com.Parameters.AddWithValue("@lastName", obj.LastName);
            com.Parameters.AddWithValue("@email", obj.Email);
            com.Parameters.AddWithValue("@phoneNumber", obj.PhoneNumber);
            com.Parameters.AddWithValue("@status", obj.Status);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }
        //To view employee details with generic list     
        public List<ContModel> GetAllContacts()
        {
            connection();
            List<ContModel> EmpList = new List<ContModel>();


            SqlCommand com = new SqlCommand("GetContacts", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                EmpList.Add(

                    new ContModel
                    {

                        Id = Convert.ToInt32(dr["Id"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                        Status = Convert.ToString(dr["Status"])

                    }
                    );
            }

            return EmpList;
        }
        //To Update Employee details    
        public bool UpdateContact(ContModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateContact", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@firstName", obj.FirstName);
            com.Parameters.AddWithValue("@lastName", obj.LastName);
            com.Parameters.AddWithValue("@email", obj.Email);
            com.Parameters.AddWithValue("@phoneNumber", obj.PhoneNumber);
            com.Parameters.AddWithValue("@status", obj.Status);
            com.Parameters.AddWithValue("@id", obj.Id);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        //To delete Employee details    
        public bool DeleteContact(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteContact", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}