using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bowling_kai
{
    public partial class Form2 : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loadイベント
        /// </summary>
        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを 'bowling_kaiDataSet.ボウリング' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.ボウリングTableAdapter.Fill(this.bowling_kaiDataSet.ボウリング);
        }

        /// <summary>
        /// 「戻る」ボタン押下時
        /// </summary>
        private void btnBack_Click(object sender, EventArgs e)
        {
            //Form2を閉じる
            this.Close();
        }

        private void ボウリングBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.ボウリングBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bowling_kaiDataSet);

        }


        /// <summary>
        /// 「削除」ボタン押下時
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection src = ボウリングDataGridView.SelectedRows;
            try
            {
                //末尾から検索し、行を削除する
                for (int i = src.Count - 1; i >= 0; i--)
                {
                    ボウリングDataGridView.Rows.RemoveAt(src[i].Index);
                    
                }ボウリングTableAdapter.Update(bowling_kaiDataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
