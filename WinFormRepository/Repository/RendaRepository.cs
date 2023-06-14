﻿using System;
using System.Data;
using System.Data.SQLite;
using WinFormDomain.Models;
using WinFormInfrastructure.Data;
using WinFormRepository.InterfaceRepository;

namespace WinFormRepository.Repository
{
    public class RendaRepository : IRendaRepository
    {
        public int InsertRenda(Renda renda)
        {
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    var sql = "INSERT INTO [Renda] ([ValorRenda] ,[TipoRenda] ,[DataEntrada]) VALUES  (@ValorRenda, @TipoRenda, @DataEntrada);";
                    context.CommandText = sql;
                    context.Parameters.AddWithValue("@ValorRenda", renda.ValorRenda);
                    context.Parameters.AddWithValue("@TipoRenda", renda.TipoRenda);
                    context.Parameters.AddWithValue("@DataEntrada", renda.DataEntrada);
                    context.ExecuteNonQuery();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetRendaAll()
        {
            SQLiteDataAdapter adapter = null;
            DataTable dataTable = new DataTable();
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    context.CommandText = "SELECT ValorRenda FROM [Renda];";
                    adapter = new SQLiteDataAdapter(context.CommandText, WinFormDbContext.DbConnection());
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetAllRendaPorData(DateTime inicail, DateTime fim)
        {
            SQLiteDataAdapter adapter = null;
            DataTable dataTable = new DataTable();
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    var param0 = string.Format($"{inicail.ToString("yyyy-MM-dd")} 00:00:00");
                    var param1 = string.Format($"{fim.ToString("yyyy-MM-dd")} 23:59:00");

                    context.CommandText = String.Format($"SELECT * FROM [Renda] WHERE DataEntrada >= '{param0}' and DataEntrada < '{param1}';");
                    adapter = new SQLiteDataAdapter(context.CommandText, WinFormDbContext.DbConnection());
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetRendaAnoAtualall()
        {
            SQLiteDataAdapter adapter = null;
            DataTable dataTable = new DataTable();
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    var param0 = DateTime.Now.Date.Year.ToString();
                    var param1 = DateTime.Now.Date.AddYears(1).Year.ToString();
                    context.CommandText = String.Format($"SELECT ValorRenda FROM [Renda] WHERE DataEntrada >'{param0}-01-01 00:00:00' and DataEntrada<'{param1}-01-01 00:00:00';");
                    adapter = new SQLiteDataAdapter(context.CommandText, WinFormDbContext.DbConnection());
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GetRendaMesAtualAll()
        {
            SQLiteDataAdapter adapter = null;
            DataTable dataTable = new DataTable();
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    var param0 = string.Format($"{DateTime.Now.Date.Year}-0{DateTime.Now.Date.Month}-01 00:00:00");
                    var param1 = string.Format($"{DateTime.Now.Date.Year}-0{DateTime.Now.Date.AddMonths(1).Month}-01 00:00:00");

                    context.CommandText = String.Format($"SELECT ValorRenda FROM [Renda] WHERE DataEntrada >= '{param0}' and DataEntrada < '{param1}';");
                    adapter = new SQLiteDataAdapter(context.CommandText, WinFormDbContext.DbConnection());
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateRenda(Renda renda)
        {
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    var sql = "UPDATE [Renda] SET [ValorRenda] = @ValorRenda ,[TipoRenda] = @TipoRenda,[DataEntrada] = @DataEntrada WHERE [IdRenda] == @IdRenda;";
                    context.CommandText = sql;
                    context.Parameters.AddWithValue("@IdRenda", renda.IdRenda);
                    context.Parameters.AddWithValue("@ValorRenda", renda.ValorRenda);
                    context.Parameters.AddWithValue("@TipoRenda", renda.TipoRenda);
                    context.Parameters.AddWithValue("@DataEntrada", renda.DataEntrada);
                    context.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteRenda(Int64 idRenda)
        {
            try
            {
                using (var context = WinFormDbContext.DbConnection().CreateCommand())
                {
                    var sql = "DELETE FROM [Renda] WHERE [IdRenda] == @IdRenda;";
                    context.CommandText = sql;
                    context.Parameters.AddWithValue("@IdRenda", idRenda);
                    context.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}