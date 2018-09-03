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
        public int frame;
        /// <summary>
        /// 1ゲームの合計値
        /// </summary>
        public int game;
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

        /// <summary>
        /// 2投目のスコア
        /// </summary>
        public string Roll2
        {
            set
            {
                if(value == "/")
                {
                    this.score.roll2 = 10 - score.roll1;
                }
                else if(value == "-")
                {
                    this.score.roll2 = 0;
                }
                else
                {
                    this.score.roll2 = int.Parse(value);
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
            lbl1_sum.Text = "";
            cmb1_2.Text = "";

            //プロパティを呼び出す
            Roll1 = cmb1_1.Text;

            //1投目がストライクだった場合、2投目は入力できないようにする処理
            if (cmb1_1.Text == "X")
            {
                cmb1_2.Enabled = false;
            }
            else
            {
                cmb1_2.Enabled = true;
            }

            score.frame = score.roll1;
            score.game = score.frame;
        }

        /// <summary>
        /// コンボボックス1_2選択時
        /// </summary>
        private void cmb1_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //プロパティを呼び出す
            Roll1 = cmb1_1.Text;
            Roll2 = cmb1_2.Text;

            score.frame = score.roll1 + score.roll2;
            score.game = score.frame;

            //2投目がスペアではなかった場合、1フレームの合計値を出力
            if (cmb1_2.Text != "/")
            {
                lbl1_sum.Text = score.game.ToString();
            }
            else
            {
                lbl1_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス2_1選択時
        /// </summary>
        private void cmb2_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //2投目から入力した場合の初期化処理
            lbl2_sum.Text = "";
            cmb2_2.Text = "";

            //プロパティを呼び出す
            Roll1 = cmb2_1.Text;

            //前フレームの1投目がストライクだった場合
            if(cmb1_1.Text == "X")
            {
                //1投目がストライクだった場合 ダブル
                if(cmb2_1.Text == "X")
                {
                    score.frame = 10 + score.roll1;
                    cmb2_2.Enabled = false;
                }
            }
            //前フレームの2投目がスペアだった場合
            else if(cmb1_2.Text == "/")
            {
                //1投目がストライクだった場合
                if (cmb2_1.Text == "X")
                {
                    score.frame = score.roll1;

                    score.game = score.roll1 + score.frame;
                    lbl1_sum.Text = score.game.ToString();

                    cmb2_2.Enabled = false;
                }
                //1投目がストライクではなかった場合
                else
                {
                    score.frame = 10 + score.roll1;
                    lbl1_sum.Text = score.frame.ToString();
                }
            }
            //どれでもなかった場合
            else
            {
                //1投目がストライクだった場合
                if (cmb2_1.Text == "X")
                {
                    score.frame = 10 + score.game;

                    cmb2_2.Enabled = false;
                }
            }
        }

        /// <summary>
        /// コンボボックス2_2選択時
        /// </summary>
        private void cmb2_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //プロパティを呼び出す
            Roll1 = cmb2_1.Text;
            Roll2 = cmb2_2.Text;

            //前フレームの1投目がストライクだった場合
            if (cmb1_1.Text == "X")
            {
                score.game = 10 + score.roll1 + score.roll2;
                lbl1_sum.Text = score.game.ToString();

                //2投目がスペアではなかった場合
                if (cmb2_2.Text != "/")
                {
                    score.game += score.roll1 + score.roll2;
                    lbl2_sum.Text = score.game.ToString();
                }
            }
            //前フレームの1投目がストライクではなかった場合
            else
            {
                score.game = score.roll1 + score.roll2 + score.frame;

                lbl2_sum.Text = score.game.ToString();
            }
        }

        /// <summary>
        /// コンボボックス3_1選択時
        /// </summary>
        private void cmb3_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //2投目から入力した場合の初期化処理
            lbl3_sum.Text = "";
            cmb3_2.Text = "";

            //プロパティを呼び出す
            Roll1 = cmb3_1.Text;

            //前フレームの1投目がストライクだった場合
            if (cmb2_1.Text == "X")
            {
                //前々フレームの1投目がストライクだった場合　ダブル
                if (cmb1_1.Text == "X")
                {
                    score.frame = 10 + score.roll1 + score.game;
                    lbl1_sum.Text = score.frame.ToString();
                }
                //前々フレームの1投目がストライクではなかった場合
                else
                {
                    score.frame = 10 + score.game;
                }
            }
            //前フレームの2投目がスペアだった場合
            else if (cmb2_2.Text == "/")
            {
                //1投目がストライクだった場合
                if (cmb3_1.Text == "X")
                {
                    score.frame = score.roll1;

                    score.game = score.roll1 + score.frame;
                    lbl2_sum.Text = score.game.ToString();
                }
                //1投目がストライクではなかった場合
                else
                {
                    score.frame = 10 + score.roll1;
                    lbl2_sum.Text = score.frame.ToString();
                }
            }
            //どれでもなかった場合
            else
            {
                //1投目がストライクだった場合
                if(cmb3_1.Text == "X")
                {

                }
            }
        }

        /// <summary>
        /// コンボボックス3_2選択時
        /// </summary>
        private void cmb3_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //プロパティを呼び出す
            Roll1 = cmb3_1.Text;
            Roll2 = cmb3_2.Text;

            //前フレームの1投目がストライクだった場合
            if(cmb2_1.Text == "X")
            {
                //前々フレームの1投目がストライクだった場合　ダブル
                if(cmb1_1.Text == "X")
                {
                    //lbl2_sumに出力
                    score.game = 10 + score.roll1 + score.roll2 + score.frame;
                    lbl2_sum.Text = score.game.ToString();

                    //lbl3_sumに出力
                    score.frame = score.game;
                    score.game = score.roll1 + score.roll2 + score.frame;
                    score.frame = score.frame - 10 - score.roll1 - score.roll2; //加算対策

                    //2投目がスペアだった場合
                    if (cmb3_2.Text == "/")
                    {
                        lbl3_sum.Text = "";
                    }
                    //2投目がスペアではなかった場合
                    else
                    {
                        lbl3_sum.Text = score.game.ToString();
                    }
                }
                //前々フレームの1投目がストライクではなかった場合
                else
                {
                    //lbl2_sumに出力
                    score.game = score.roll1 + score.roll2 + score.frame;
                    lbl2_sum.Text = score.game.ToString();

                    //lbl3_sumに出力
                    score.frame = score.game;
                    score.game = score.roll1 + score.roll2 + score.frame;

                    //2投目がスペアだった場合
                    if (cmb3_2.Text == "/")
                    {
                        lbl3_sum.Text = "";
                    }
                    //2投目がスペアではなかった場合
                    else
                    {
                        lbl3_sum.Text = score.game.ToString();
                    }
                    score.frame = score.frame - score.roll1 - score.roll2;  //加算対策
                }
            }
            //前フレームの1投目がストライクではなかった場合
            else
            {
                score.frame = score.game;
                score.game = score.roll1 + score.roll2 + score.frame;

                score.frame = score.frame - score.roll1 - score.roll2;      //加算対策

                lbl3_sum.Text = score.game.ToString();
            }
        }

        #endregion


        #region メソッド

        /// <summary>
        /// コンボボックス初期化処理
        /// </summary>
        private void Print()
        {
            int i = 1;

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
