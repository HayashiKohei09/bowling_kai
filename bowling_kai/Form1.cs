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
    public partial class Form1 : Form
    {
        /// <summary>
        /// スコアを格納する配列
        /// </summary>
        private int[] _score = new int[20];

        /// <summary>
        /// 1フレームの合計値
        /// </summary>
        private int _frame = 0;

        /// <summary>
        /// 1ゲームの合計値
        /// </summary>
        private int _total = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Form1()
        {
            InitializeComponent();
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
            lbl1_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[0] = Point(cmb1_1.Text, 0);

            _frame = _score[0];
            _total = _score[0];

            //1投目がストライクだった場合、2投目選択不可
            if (cmb1_1.Text == "X")
            {
                cmb1_2.Enabled = false;
                cmb1_2.Text = "";
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb1_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス1_2選択時
        /// </summary>
        private void cmb1_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[1] = Point(cmb1_2.Text, 0);

            _frame = _score[0] + _score[1];
            _total = _score[0] + _score[1];

            if(cmb1_2.Text != "/")
            {
                lbl1_sum.Text = _total.ToString();
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
            lbl2_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[2] = Point(cmb2_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb2_1.Text == "X")
            {
                cmb2_2.Enabled = false;
                cmb2_2.Text = "";

                _total = _score[2] + _frame;
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb2_2.Enabled = true;
            }

            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            if(cmb1_2.Text == "/")
            {
                _frame = _score[2] + _total;
                lbl1_sum.Text = _frame.ToString();
            }
        }

        /// <summary>
        /// コンボボックス2_2選択時
        /// </summary>
        private void cmb2_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[3] = Point(cmb2_2.Text, 2);

            //前フレームの1投目がストライクだった場合、ボーナス点を追加
            if (cmb1_1.Text == "X")
            {
                _total = _score[2] + _score[3] + _frame;
                lbl1_sum.Text = _total.ToString();

                _total = _score[2] + _score[3] + _total;
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                _frame = int.Parse(lbl1_sum.Text);
                _total = _score[2] + _score[3] + _frame;
            }
            

            if (cmb2_2.Text != "/")
            {
                lbl2_sum.Text = _total.ToString();
            }
            else
            {
                lbl2_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス3_1選択時
        /// </summary>
        private void cmb3_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl3_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[4] = Point(cmb3_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb3_1.Text == "X")
            {
                cmb3_2.Enabled = false;
                cmb3_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if(cmb2_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if(cmb1_1.Text == "X")
                    {
                        _frame = _score[4] + _total;
                        lbl1_sum.Text = _frame.ToString();
                    }
                    //前々フレームの1投目がストライクではなかった場合、2フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[4] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if(cmb2_2.Text == "/")
                {
                    _frame = _score[4] + _total;
                    lbl2_sum.Text = _frame.ToString();
                }
            }
            //前フレームの1投目がストライクだった場合
            else if(cmb2_1.Text == "X")
            {
                cmb3_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb1_1.Text == "X")
                {
                    _frame = _score[4] + _total;
                    lbl1_sum.Text = _frame.ToString();
                }
                //前々フレームの1投目がストライクではなかった場合、3フレーム目の合計値を蓄積
                else
                {
                    _frame = _total;
                }
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb3_2.Enabled = true;
            }

            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            if (cmb2_2.Text == "/")
            {
                _frame = _score[4] + _total;
                lbl2_sum.Text = _frame.ToString();
            }
        }

        /// <summary>
        /// コンボボックス3_2選択時
        /// </summary>
        private void cmb3_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[5] = Point(cmb3_2.Text, 4);

            //前フレームの1投目がストライクだった場合
            if (cmb2_1.Text == "X")
            {
                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb1_1.Text == "X")
                {
                    _total = _score[3] + _score[4] + _score[5] + _frame;
                    lbl2_sum.Text = _total.ToString();

                    _total = _score[4] + _score[5] + _total;
                }
                //前フレームの1投目がストライクだった場合、ボーナス点を追加
                else
                {
                    _total = _score[4] + _score[5] + _frame;
                    lbl2_sum.Text = _total.ToString();

                    _total = _score[4] + _score[5] + _total;
                }
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                _frame = int.Parse(lbl2_sum.Text);
                _total = _score[4] + _score[5] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb3_2.Text != "/")
            {
                lbl3_sum.Text = _total.ToString();
            }
            else
            {
                lbl3_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス4_1選択時
        /// </summary>
        private void cmb4_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl4_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[6] = Point(cmb4_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb4_1.Text == "X")
            {
                cmb4_2.Enabled = false;
                cmb4_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb3_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb2_1.Text == "X")
                    {
                        _total = int.Parse(lbl1_sum.Text);
                        _frame = _score[2] + _score[4] + _score[6] + _total;
                        lbl2_sum.Text = _frame.ToString();
                    }
                    //前々フレームの1投目がストライクではなかった場合、3フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[6] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb3_2.Text == "/")
                {
                    _frame = _score[6] + _total;
                    lbl3_sum.Text = _frame.ToString();

                    _frame = _score[6] + _frame;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb3_1.Text == "X")
            {
                cmb4_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb2_1.Text == "X")
                {
                    _frame = 10 + _score[6] + _total;
                    lbl2_sum.Text = _frame.ToString();
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb3_2.Text == "/")
            {
                _frame = _score[6] + _total;
                lbl3_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb4_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス4_2選択時
        /// </summary>
        private void cmb4_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[7] = Point(cmb4_2.Text, 6);

            //前フレームの1投目がストライクだった場合
            if (cmb3_1.Text == "X")
            {
                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb2_1.Text == "X")
                {
                    _total = _score[5] + _score[6] + _score[7] + _frame;
                    lbl3_sum.Text = _total.ToString();

                    _total = _score[6] + _score[7] + _total;
                }
                //前フレームの1投目がストライクだった場合、ボーナス点を追加
                else
                {
                    _total = _score[6] + _score[7] + _frame;
                    lbl3_sum.Text = _total.ToString();

                    _total = _score[6] + _score[7] + _total;
                }
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                _frame = int.Parse(lbl3_sum.Text);
                _total = _score[6] + _score[7] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb4_2.Text != "/")
            {
                lbl4_sum.Text = _total.ToString();
            }
            else
            {
                lbl4_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス5_1選択時
        /// </summary>
        private void cmb5_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl5_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[8] = Point(cmb5_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb5_1.Text == "X")
            {
                cmb5_2.Enabled = false;
                cmb5_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb4_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb3_1.Text == "X")
                    {
                        _total = int.Parse(lbl2_sum.Text);
                        _frame = _score[4] + _score[6] + _score[8] + _total;
                        lbl3_sum.Text = _frame.ToString();
                    }
                    //前々フレームの1投目がストライクではなかった場合、4フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[8] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb4_2.Text == "/")
                {
                    _frame = _score[8] + _total;
                    lbl4_sum.Text = _frame.ToString();
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb4_1.Text == "X")
            {
                cmb5_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb3_1.Text == "X")
                {
                    _frame = 10 + _score[8] + _total;
                    lbl3_sum.Text = _frame.ToString();
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb4_2.Text == "/")
            {
                _frame = _score[8] + _total;
                lbl4_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb5_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス5_2選択時
        /// </summary>
        private void cmb5_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[9] = Point(cmb5_2.Text, 8);

            //前フレームの1投目がストライクだった場合
            if (cmb4_1.Text == "X")
            {
                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb3_1.Text == "X")
                {
                    _total = _score[7] + _score[8] + _score[9] + _frame;
                    lbl4_sum.Text = _total.ToString();

                    _total = _score[8] + _score[9] + _total;
                }
                //前フレームの1投目がストライクだった場合、ボーナス点を追加
                else
                {
                    _total = _score[8] + _score[9] + _frame;
                    lbl4_sum.Text = _total.ToString();

                    _total = _score[8] + _score[9] + _total;
                }
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                _frame = int.Parse(lbl4_sum.Text);
                _total = _score[8] + _score[9] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb5_2.Text != "/")
            {
                lbl5_sum.Text = _total.ToString();
            }
            else
            {
                lbl5_sum.Text = "";
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

        /// <summary>
        /// //スコア代入処理
        /// </summary>
        /// <param name="str">コンボボックス選択時の文字・数字</param>
        /// <param name="i">1投目スコアの添字</param>
        /// <returns></returns>
        private int Point(string str, int i)
        {
            if (str == "X")
            {
                return 10;
            }
            else if (str == "G" || str == "-")
            {
                return 0;
            }
            else if (str == "/")
            {
                return 10 - _score[i];
            }
            else
            {
                return int.Parse(str);
            }
        }



        #endregion        
    }
}
