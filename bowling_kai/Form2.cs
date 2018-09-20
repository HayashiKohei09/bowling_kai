//Version 1.0.0 2018/09/20

using System;
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

        /// <summary>
        /// 「保存」アイコン押下時
        /// </summary>
        private void ボウリングBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("更新してもよろしいですか？",
                                                      "更新確認",
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Exclamation);
                
                if (result == DialogResult.OK)
                {
                    this.Validate();
                    this.ボウリングBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.bowling_kaiDataSet);

                    MessageBox.Show("更新が完了しました。",
                                    "更新完了",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,
                                "エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
