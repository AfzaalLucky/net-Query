﻿namespace QckQuery
{
    using DataTableToObject;
    using DataTableToObject.Exceptions;
    using QckQuery.DataAccess;
    using QckQuery.Formatting;
    using System.Collections.Generic;
    using System.Data;

    public partial class QuickQuery
    {
        /// <summary>
        /// Runs the given query and returns the queried values.
        /// It uses DbDataAdapter.Fill underneath.
        /// </summary>
        /// <param name="sql">Query to run</param>
        /// <param name="parameters">Parameters names and values pairs</param>
        /// <returns>A DataTable with the queried values</returns>
        public DataTable WithReturn(string sql, object parameters)
        {
            return WithReturn(sql, DictionaryMaker.Make(parameters));
        }

        /// <summary>
        /// Runs the given query and returns the queried values.
        /// It uses DbDataAdapter.Fill underneath.
        /// </summary>
        /// <param name="sql">Query to run</param>
        /// <param name="parameters">Parameters names and values pairs</param>
        /// <returns>A DataTable with the queried values</returns>
        /// <exception cref="MismatchedTypesException">
        /// The corresponding type in the given class is different than the one found in the DataTable
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        /// A column of the DataTable doesn't match any in the given class
        /// </exception>
        /// <exception cref="MismatchedTypesException">
        /// The corresponding type in the given class is different than the one found in the DataTable
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        /// A column of the DataTable doesn't match any in the given class
        /// </exception>
        public IEnumerable<T> WithReturn<T>(string sql, object parameters) where T : new()
        {
            return WithReturn(sql, DictionaryMaker.Make(parameters)).ToObject<T>();
        }

        /// <summary>
        /// Runs the given query and returns the queried values.
        /// It uses DbDataAdapter.Fill underneath.
        /// </summary>
        /// <param name="sql">Query to run</param>
        /// <param name="parameters">Parameters names and values pairs</param>
        /// <returns>A DataTable with the queried values</returns>
        public DataTable WithReturn(string sql, params object[] parameters)
        {
            return WithReturn(sql, DictionaryMaker.Make(parameters));
        }

        /// <summary>
        /// Runs the given query and returns the queried values.
        /// It uses DbDataAdapter.Fill underneath.
        /// </summary>
        /// <param name="sql">Query to run</param>
        /// <param name="parameters">Parameters names and values pairs</param>
        /// <returns>A DataTable with the queried values</returns>
        /// <exception cref="MismatchedTypesException">
        /// The corresponding type in the given class is different than the one found in the DataTable
        /// </exception>
        /// <exception cref="PropertyNotFoundException">
        /// A column of the DataTable doesn't match any in the given class
        /// </exception>
        public IEnumerable<T> WithReturn<T>(string sql, params object[] parameters) where T : new()
        {
            return WithReturn(sql, DictionaryMaker.Make(parameters)).ToObject<T>();
        }

        private DataTable WithReturn(string sql, IDictionary<string, object> parameters)
        {
            using (var connection = _connectionProvider.Provide())
            using (var command = connection.GetCommandWithParametersSet(sql, parameters))
            {
                return _dataTableFiller.Fill(command);
            }
        }
    }
}