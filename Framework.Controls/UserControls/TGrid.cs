using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;

namespace Framework.Controls.UserControls
{
    public partial class TGrid : DataGrid, INormalControls
    {
        private bool _isDirty;
        private DataTable sourceTable;

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
            }
        }

        public DataTable DataTable
        {
            get { return sourceTable; }
            set
            {
                //if (value == null)
                //{
                //    throw new ArgumentNullException("The source table cannot be null.");
                //}

                ////Unhook a previously assigned DataTable
                //if (sourceTable != null)
                //{
                //    sourceTable.RowChanged -= new DataRowChangeEventHandler(OnRowChanged);
                //    sourceTable.RowDeleted -= new DataRowChangeEventHandler(OnRowDeleted);
                //    sourceTable.TableNewRow -= new DataTableNewRowEventHandler(OnTableNewRow);
                //}

                //sourceTable = value;

                //sourceTable.RowChanged += new DataRowChangeEventHandler(OnRowChanged);
                //sourceTable.RowDeleted += new DataRowChangeEventHandler(OnRowDeleted);
                //sourceTable.TableNewRow += new DataTableNewRowEventHandler(OnTableNewRow);

                if (value != null)
                {
                    sourceTable = value;
                }
            }
        }

        /// <summary>
        /// Handler for when the row is actually added to the DataTable's row collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowChanged(object sender, DataRowChangeEventArgs e)
        {
            this._isDirty = true;
            //System.Console.WriteLine("DataGrid OnRowChanged");
        }

        /// <summary>
        /// The row deleted event fires when the row has being removed from the collection.
        /// We can't use the row deleted event to record the row field values because the row
        /// has been then marked as deleted and accessing the fields throws an exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleted(object sender, DataRowChangeEventArgs e)
        {
            this._isDirty = true;
            //System.Console.WriteLine("DataGrid OnRowDeleted");
        }

        /// <summary>
        /// Log the new row and add it to the uncommitted row collection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnTableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            this._isDirty = true;
            //System.Console.WriteLine("DataGrid OnTableNewRow");
        }

        public TGrid()
        {
            InitializeComponent();
            initializeTGrid();
        }

        public object this[int rowIndex, string columnName]
        {
            get
            {
                // Find column index corresponding to column name
                int columnIndex = -1;
                DataGridTableStyle tableStyle = this.TableStyles[0];
                GridColumnStylesCollection gridColStyle = tableStyle.GridColumnStyles;
                if (gridColStyle != null)
                {
                    for (int i = 0; i < gridColStyle.Count; i++)
                    {
                        string headerNm = gridColStyle[i].HeaderText;
                        if (columnName.Equals(headerNm))
                        {
                            columnIndex = i;
                            break;
                        }
                    }
                }
                if (columnIndex >= 0)
                {
                    return base[rowIndex, columnIndex];
                }
                return null;
            }
            set
            {
                // No implementation needed.
            }
        }

        private void initializeTGrid()
        {
            sourceTable = new DataTable();
            sourceTable.RowChanged += new DataRowChangeEventHandler(OnRowChanged);
            sourceTable.RowDeleted += new DataRowChangeEventHandler(OnRowDeleted);
            sourceTable.TableNewRow += new DataTableNewRowEventHandler(OnTableNewRow);
            this.DataSource = sourceTable;
        }

        public void setHeader(String[,] header)
        {
            DataGridTableStyle tableStyle = new DataGridTableStyle();
            //////DataTable = new DataTable();
            ////sourceTable = new DataTable();
            ////sourceTable.RowChanged += new DataRowChangeEventHandler(OnRowChanged);
            ////sourceTable.RowDeleted += new DataRowChangeEventHandler(OnRowDeleted);
            ////sourceTable.TableNewRow += new DataTableNewRowEventHandler(OnTableNewRow);

            this.TableStyles.Add(tableStyle);
            ////this.DataSource = DataTable;
            //this.DataSource = sourceTable;

            DataColumn cColumn;
            for (int i = 0; i < header.Length / 2; i++)
            {
                cColumn = new DataColumn(header[i, 0]);
                cColumn.ReadOnly = true;
                //cColumn.ReadOnly = false;

                //DataTable.Columns.Add(cColumn);
                sourceTable.Columns.Add(cColumn);
                tableStyle.GridColumnStyles.Add(new DataGridTextBoxColumn());
                tableStyle.GridColumnStyles[i].HeaderText = header[i, 0];
                tableStyle.GridColumnStyles[i].MappingName = header[i, 0];
                tableStyle.GridColumnStyles[i].Width = Convert.ToInt16(header[i, 1]);
                tableStyle.GridColumnStyles[i].NullText = "";
            }
        }

        /*
        public void setHeaderVSR(String[,] header, String mapping)
        {
            DataGridTableStyle tableStyle = new DataGridTableStyle();
            //////DataTable = new DataTable();
            ////sourceTable = new DataTable();
            ////sourceTable.RowChanged += new DataRowChangeEventHandler(OnRowChanged);
            ////sourceTable.RowDeleted += new DataRowChangeEventHandler(OnRowDeleted);
            ////sourceTable.TableNewRow += new DataTableNewRowEventHandler(OnTableNewRow);


            //this.TableStyles.Add(tableStyle);
            ////this.DataSource = DataTable;
            //this.DataSource = sourceTable;
            tableStyle.MappingName = mapping;
            //DataColumn cColumn;
            for (int i = 0; i < header.Length / 2; i++)
            {
                //cColumn = new DataColumn(header[i, 0]);
                //cColumn.ReadOnly = true;
                //cColumn.ReadOnly = false;

                //DataTable.Columns.Add(cColumn);
                //sourceTable.Columns.Add(cColumn);

                //tableStyle.GridColumnStyles.Add(new DataGridTextBoxColumn());
                //tableStyle.GridColumnStyles[i].HeaderText = header[i, 0];
                //tableStyle.GridColumnStyles[i].MappingName = header[i, 0];
                //tableStyle.GridColumnStyles[i].Width = Convert.ToInt16(header[i, 1]);
                //tableStyle.GridColumnStyles[i].NullText = "";

                DataGridTextBoxColumn tbcName = new DataGridTextBoxColumn();
                tbcName.Width = Convert.ToInt16(header[i, 1]);
                tbcName.MappingName = header[i, 0];
                tbcName.HeaderText = header[i, 0];
                tableStyle.GridColumnStyles.Add(tbcName);
            }

            this.TableStyles.Clear();
            this.TableStyles.Add(tableStyle);
        }
        */
        public void displayData(DataTable dataList)
        {
            //this.DataTable = dataList;
            this.sourceTable = dataList;
        }

        public DataRow NewRow()
        {
            //return DataTable.NewRow();
            return sourceTable.NewRow();
        }

        public void Add(DataRow newRow)
        {
            //DataTable.Rows.Add(newRow);
            sourceTable.Rows.Add(newRow);
        }

        public void Clear()
        {
            //DataTable.Rows.Clear();
            sourceTable.Rows.Clear();
        }

        private void TGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.CurrentRowIndex >= 0)
            {
                this.Select(this.CurrentRowIndex);
                Invalidate();
            }
        }
    }
}
