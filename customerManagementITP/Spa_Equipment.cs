﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using customerManagementITP;

namespace SPA
{
    class Spa_Equipment
    {

        private String brandName;
        private String equipmentType;
        private String modelNumber;
        private String supplierNic;
        private float eunitPrice;
        private int numOfEquipment;
        private float ePrice;
        private String equipmentSearchTxt;

        private SqlConnection sqlcon = DBConnection.getConnection();

        public string BrandName { get => brandName; set => brandName = value; }
        public string EquipmentType { get => equipmentType; set => equipmentType = value; }
        public String ModelNumber { get => modelNumber; set => modelNumber = value; }
        public string SupplierNic { get => supplierNic; set => supplierNic = value; }
        public float EunitPrice { get => eunitPrice; set => eunitPrice = value; }
        public int NumOfEquipment { get => numOfEquipment; set => numOfEquipment = value; }
        public float EPrice { get => ePrice; set => ePrice = value; }
        public String EquipmentSearchTxt { get => equipmentSearchTxt; set => equipmentSearchTxt = value; }


        public void equipmentSave()
        {

            DBConnection.openDBConnection();

            SqlCommand sqlCommand = new SqlCommand("spa_AddEquipment", sqlcon);
            sqlCommand.CommandType = CommandType.StoredProcedure;


            sqlCommand.Parameters.AddWithValue("@brandName", brandName);
            sqlCommand.Parameters.AddWithValue("@equipmentType", equipmentType);
            sqlCommand.Parameters.AddWithValue("@modelNumber", modelNumber);
            sqlCommand.Parameters.AddWithValue("@supplierNic",supplierNic );
            sqlCommand.Parameters.AddWithValue("@equipmentUnitPrice",eunitPrice );
            sqlCommand.Parameters.AddWithValue("@noOfEquipment",numOfEquipment);
            sqlCommand.Parameters.AddWithValue("@etotalAmount", ePrice);
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Saved Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //store values to incomeExpences table
            SqlCommand sqlcmd1 = new SqlCommand("AddSpaExpencesToIncomeExpenses", sqlcon);
            sqlcmd1.CommandType = CommandType.StoredProcedure;

            sqlcmd1.Parameters.AddWithValue("@Expense", ePrice);

            sqlcmd1.ExecuteNonQuery();

            DBConnection.closeDBConnection();

        }


        public DataTable equipmentDataView()
        {
            DBConnection.openDBConnection();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("spa_viewSearchEquipment", sqlcon);
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTxt", equipmentSearchTxt);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            DBConnection.closeDBConnection();
            return dataTable;

        }

        public void equipmentUpdate(int entryId)
        {

            DBConnection.openDBConnection();

            SqlCommand sqlCommand = new SqlCommand("spa_EditEquipment", sqlcon);
            sqlCommand.CommandType = CommandType.StoredProcedure;
           
            sqlCommand.Parameters.AddWithValue("@entryId", entryId);
            sqlCommand.Parameters.AddWithValue("@brandName", brandName);
            sqlCommand.Parameters.AddWithValue("@equipmentType", equipmentType);
            sqlCommand.Parameters.AddWithValue("@modelNumber", modelNumber);
            sqlCommand.Parameters.AddWithValue("@supplierNic", supplierNic);
            sqlCommand.Parameters.AddWithValue("@equipmentUnitPrice", eunitPrice);
            sqlCommand.Parameters.AddWithValue("@noOfEquipment", numOfEquipment);
            sqlCommand.Parameters.AddWithValue("@etotalAmount", ePrice);

            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Updated Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DBConnection.closeDBConnection();

        }

        public void equipmentDelete(int entryId)
        {

            DBConnection.openDBConnection();
            SqlCommand sqlCommand = new SqlCommand("spa_DeleteEquipment", sqlcon);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@entryId", entryId);
            sqlCommand.ExecuteNonQuery();
            MessageBox.Show("Deleted Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DBConnection.closeDBConnection();
        }

    }

}
