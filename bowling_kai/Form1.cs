using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bowling_kai
{
    #region 構造体 Score

    struct Score
    {
        /// <summary>
        /// 1投目
        /// </summary>
        public int roll1;
        /// <summary>
        /// 2投目
        /// </summary>
        public int roll2;
        /// <summary>
        /// 3投目(10フレームのみ使用)
        /// </summary>
        public int roll3;
        /// <summary>
        /// 1フレームの合計値
        /// </summary>
        public int score;
        /// <summary>
        /// 1ゲームの合計値
        /// </summary>
        public int total;
    }

    #endregion

    public partial class Form1 : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 構造体の宣言
        /// </summary>
        Score score;

        /// <summary>
        /// 1投目のスコア
        /// </summary>
        public string Roll1
        {
            set
            {
                if (value == "X")
                {
                    this.score.roll1 = 10;
                }
                else if (value == "G")
                {
                    this.score.roll1 = 0;
                }
                else
                {
                    this.score.roll1 = int.Parse(value);
                }
            }
        }


        #region イベント

        /// <summary>
        /// Loadイベント
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //コンボボックス初期化処理
            Print();
        }

        /// <summary>
        /// コンボボックス1_1選択時
        /// </summary>
        private void cmb1_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //2投目から入力した場合の初期化処理
            lbl1f.Text = "";
            cmb1_2.SelectedIndex = -1;
            
            //プロパティを呼び出す
            Roll1 = cmb1_1.Text;
            

        }

        #endregion


        #region メソッド

        /// <summary>
        /// コンボボックス初期化処理
        /// </summary>
        private void Print()
        {
            int i = 0;

            //0～9までの数字を印字。
            while(i<10)
            {
                //コンボボックス追加処理(数字)
                Addnum(i);
                i++;
            }

            //ストライク印字
            string str = "X";
            //コンボボックス追加処理(文字)
            Addstr(str);

            //スペア印字
            str = "/";
            //コンボボックス追加処理(文字)
            Addstr(str);

            //ガター印字
            str = "G";
            //コンボボックス追加処理(文字)
            Addstr(str);

            //ミス印字
            str = "-";
            //コンボボックス追加処理(文字)
            Addstr(str);
        }

        /// <summary>
        /// コンボボックス追加処理(数字)
        /// </summary>
        /// <param name="i">スコア(数字)</param>
        private void Addnum(int i)
        {
            cmb1_1.Items.Add(i);
            cmb1_2.Items.Add(i);
            cmb2_1.Items.Add(i);
            cmb2_2.Items.Add(i);
            cmb3_1.Items.Add(i);
            cmb3_2.Items.Add(i);
            cmb4_1.Items.Add(i);
            cmb4_2.Items.Add(i);
            cmb5_1.Items.Add(i);
            cmb5_2.Items.Add(i);
            cmb6_1.Items.Add(i);
            cmb6_2.Items.Add(i);
            cmb7_1.Items.Add(i);
            cmb7_2.Items.Add(i);
            cmb8_1.Items.Add(i);
            cmb8_2.Items.Add(i);
            cmb9_1.Items.Add(i);
            cmb9_2.Items.Add(i);
            cmb10_1.Items.Add(i);
            cmb10_2.Items.Add(i);
            cmb10_3.Items.Add(i);
        }

        /// <summary>
        /// コンボボックス追加処理(文字)
        /// </summary>
        /// <param name="str">スコア(文字)</param>
        private void Addstr(string str)
        {
            if (str == "X" || str == "G")
            {
                cmb1_1.Items.Add(str);
                cmb2_1.Items.Add(str);
                cmb3_1.Items.Add(str);
                cmb4_1.Items.Add(str);
                cmb5_1.Items.Add(str);
                cmb6_1.Items.Add(str);
                cmb7_1.Items.Add(str);
                cmb8_1.Items.Add(str);
                cmb9_1.Items.Add(str);
                cmb10_1.Items.Add(str);

                if (str == "X")
                {
                    cmb10_2.Items.Add(str);
                    cmb10_3.Items.Add(str);
                }
            }

            else if(str == "/" || str == "-")
            {
                cmb1_2.Items.Add(str);
                cmb2_2.Items.Add(str);
                cmb3_2.Items.Add(str);
                cmb4_2.Items.Add(str);
                cmb5_2.Items.Add(str);
                cmb6_2.Items.Add(str);
                cmb7_2.Items.Add(str);
                cmb8_2.Items.Add(str);
                cmb9_2.Items.Add(str);
                cmb10_2.Items.Add(str);
                cmb10_3.Items.Add(str);
            }
        }

        #endregion
    }
}
