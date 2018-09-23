//Version 1.0.0 2018/09/20
//Version 1.0.1 2018/09/22
//Version 1.0.2 2018/09/23
//Version 1.0.3 2018/09/23

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
            // TODO: このコード行はデータを 'bowlingDataSet.Bowling_Table' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.bowling_TableTableAdapter.Fill(this.bowlingDataSet.Bowling_Table);
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
        /// 「削除」アイコン押下時
        /// </summary>
        private void bindingNavigatorDeleteItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("削除しました。\n確定する場合は保存アイコンをクリックし更新を行ってください。",
                            "削除しました",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// 「保存」アイコン押下時
        /// </summary>
        private void bowling_TableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("スコアを更新します。\nよろしいですか？",
                                                  "スコア更新",
                                                  MessageBoxButtons.OKCancel,
                                                  MessageBoxIcon.Exclamation);

            if (result == DialogResult.OK)
            {
                this.Validate();
                this.bowling_TableBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.bowlingDataSet);

                MessageBox.Show("更新に成功しました。",
                                "更新しました",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        
    }
}
