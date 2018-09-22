//Version 1.0.1 2018/09/22

using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bowling_kai
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// スコアを格納する配列
        /// </summary>
        private int[] _score = new int[21];

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

            //タイマー起動
            timTime.Start();
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

            //1投目がストライクだった場合
            if (cmb2_1.Text == "X")
            {
                cmb2_2.Enabled = false;
                cmb2_2.Text = "";

                //前フレームの1投目がストライクだった場合、1フレーム目の合計値を蓄積(ダブル)
                if (cmb1_1.Text == "X")
                {
                    _frame = _score[0] + _score[2];
                }
                //どれでもなかった場合、2フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[2] + _total;
                }
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
                int.TryParse(lbl1_sum.Text, out _frame);
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

            //1投目がストライクだった場合
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
                        _total = 0;

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(4, lbl1_sum);
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
                //どれでもなかった場合、3フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[4] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if(cmb2_1.Text == "X")
            {
                cmb3_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb1_1.Text == "X")
                {
                    _total = 0;

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(4, lbl1_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb2_2.Text == "/")
            {
                cmb3_2.Enabled = true;

                _frame = _score[4] + _total;
                lbl2_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb3_2.Enabled = true;
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
                //ダブル・シングルボーナス点追加処理
                Bonus2(5, lbl2_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl2_sum.Text, out _frame);
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

            //1投目がストライクだった場合
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
                        int.TryParse(lbl1_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(6, lbl2_sum);
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
                //どれでもなかった場合、4フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[6] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb3_1.Text == "X")
            {
                cmb4_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb2_1.Text == "X")
                {
                    int.TryParse(lbl1_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(6, lbl2_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb3_2.Text == "/")
            {
                cmb4_2.Enabled = true;

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
                //ダブル・シングルボーナス点追加処理
                Bonus2(7, lbl3_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl3_sum.Text, out _frame);
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
                        int.TryParse(lbl2_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(8, lbl3_sum);
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
                //どれでもなかった場合、5フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[8] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb4_1.Text == "X")
            {
                cmb5_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb3_1.Text == "X")
                {
                    int.TryParse(lbl2_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(8, lbl3_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb4_2.Text == "/")
            {
                cmb5_2.Enabled = true;

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
                //ダブル・シングルボーナス点追加処理
                Bonus2(9, lbl4_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl4_sum.Text, out _frame);
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

        /// <summary>
        /// コンボボックス6_1選択時
        /// </summary>
        private void cmb6_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl6_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[10] = Point(cmb6_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb6_1.Text == "X")
            {
                cmb6_2.Enabled = false;
                cmb6_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb5_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb4_1.Text == "X")
                    {
                        int.TryParse(lbl3_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(10, lbl4_sum);
                    }
                    //前々フレームの1投目がストライクではなかった場合、5フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[10] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb5_2.Text == "/")
                {
                    _frame = _score[10] + _total;
                    lbl5_sum.Text = _frame.ToString();
                }
                //どれでもなかった場合、6フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[10] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb5_1.Text == "X")
            {
                cmb6_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb4_1.Text == "X")
                {
                    int.TryParse(lbl3_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(10, lbl4_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb5_2.Text == "/")
            {
                cmb6_2.Enabled = true;

                _frame = _score[10] + _total;
                lbl5_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb6_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス6_2選択時
        /// </summary>
        private void cmb6_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[11] = Point(cmb6_2.Text, 10);

            //前フレームの1投目がストライクだった場合
            if (cmb5_1.Text == "X")
            {
                //ダブル・シングルボーナス点追加処理
                Bonus2(11, lbl5_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl5_sum.Text, out _frame);
                _total = _score[10] + _score[11] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb6_2.Text != "/")
            {
                lbl6_sum.Text = _total.ToString();
            }
            else
            {
                lbl6_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス7_1選択時
        /// </summary>
        private void cmb7_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl7_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[12] = Point(cmb7_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb7_1.Text == "X")
            {
                cmb7_2.Enabled = false;
                cmb7_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb6_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb5_1.Text == "X")
                    {
                        int.TryParse(lbl4_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(12, lbl5_sum);
                    }
                    //前々フレームの1投目がストライクではなかった場合、6フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[12] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb6_2.Text == "/")
                {
                    _frame = _score[12] + _total;
                    lbl6_sum.Text = _frame.ToString();
                }
                //どれでもなかった場合、7フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[12] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb6_1.Text == "X")
            {
                cmb7_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb5_1.Text == "X")
                {
                    int.TryParse(lbl4_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(12, lbl5_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb6_2.Text == "/")
            {
                cmb7_2.Enabled = true;

                _frame = _score[12] + _total;
                lbl6_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb7_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス7_2選択時
        /// </summary>
        private void cmb7_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[13] = Point(cmb7_2.Text, 12);

            //前フレームの1投目がストライクだった場合
            if (cmb6_1.Text == "X")
            {
                //ダブル・シングルボーナス点追加処理
                Bonus2(13, lbl6_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl6_sum.Text, out _frame);
                _total = _score[12] + _score[13] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb7_2.Text != "/")
            {
                lbl7_sum.Text = _total.ToString();
            }
            else
            {
                lbl7_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス8_1選択時
        /// </summary>
        private void cmb8_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl8_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[14] = Point(cmb8_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb8_1.Text == "X")
            {
                cmb8_2.Enabled = false;
                cmb8_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb7_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb6_1.Text == "X")
                    {
                        int.TryParse(lbl5_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(14, lbl6_sum);
                    }
                    //前々フレームの1投目がストライクではなかった場合、7フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[14] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb7_2.Text == "/")
                {
                    _frame = _score[14] + _total;
                    lbl7_sum.Text = _frame.ToString();
                }
                //どれでもなかった場合、8フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[14] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb7_1.Text == "X")
            {
                cmb8_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb6_1.Text == "X")
                {
                    int.TryParse(lbl5_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(14, lbl6_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb7_2.Text == "/")
            {
                cmb8_2.Enabled = true;

                _frame = _score[14] + _total;
                lbl7_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb8_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス8_2選択時
        /// </summary>
        private void cmb8_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[15] = Point(cmb8_2.Text, 14);

            //前フレームの1投目がストライクだった場合
            if (cmb7_1.Text == "X")
            {
                //ダブル・シングルボーナス点追加処理
                Bonus2(15, lbl7_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl7_sum.Text, out _frame);
                _total = _score[14] + _score[15] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb8_2.Text != "/")
            {
                lbl8_sum.Text = _total.ToString();
            }
            else
            {
                lbl8_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス9_1選択時
        /// </summary>
        private void cmb9_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl9_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[16] = Point(cmb9_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb9_1.Text == "X")
            {
                cmb9_2.Enabled = false;
                cmb9_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb8_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb7_1.Text == "X")
                    {
                        int.TryParse(lbl6_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(16, lbl7_sum);
                    }
                    //前々フレームの1投目がストライクではなかった場合、8フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[16] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb8_2.Text == "/")
                {
                    _frame = _score[16] + _total;
                    lbl8_sum.Text = _frame.ToString();
                }
                //どれでもなかった場合、9フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[16] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb8_1.Text == "X")
            {
                cmb9_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb7_1.Text == "X")
                {
                    int.TryParse(lbl6_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(16, lbl7_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb8_2.Text == "/")
            {
                cmb9_2.Enabled = true;

                _frame = _score[16] + _total;
                lbl8_sum.Text = _frame.ToString();
            }
            //1投目がストライクではなかった場合、2投目選択可
            else
            {
                cmb9_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス9_2選択時
        /// </summary>
        private void cmb9_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[17] = Point(cmb9_2.Text, 16);

            //前フレームの1投目がストライクだった場合
            if (cmb8_1.Text == "X")
            {
                //ダブル・シングルボーナス点追加処理
                Bonus2(17, lbl8_sum);
            }
            //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
            else
            {
                int.TryParse(lbl8_sum.Text, out _frame);
                _total = _score[16] + _score[17] + _frame;
            }

            //2投目がスペアではなかった場合、合計値を印字
            if (cmb9_2.Text != "/")
            {
                lbl9_sum.Text = _total.ToString();
            }
            else
            {
                lbl9_sum.Text = "";
            }
        }

        /// <summary>
        /// コンボボックス10_1選択時
        /// </summary>
        private void cmb10_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl10_sum.Text = "";

            //スコア代入処理(1投目の添え字は適当)
            _score[18] = Point(cmb10_1.Text, 0);

            //1投目がストライクだった場合、2投目選択不可
            if (cmb10_1.Text == "X")
            {
                cmb10_2.Enabled = true;
                cmb10_2.Text = "";

                //前フレームの1投目がストライクだった場合
                if (cmb9_1.Text == "X")
                {
                    //前々フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                    if (cmb8_1.Text == "X")
                    {
                        int.TryParse(lbl7_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(18, lbl8_sum);
                    }
                    //前々フレームの1投目がストライクではなかった場合、9フレーム目の合計値を蓄積(ダブル)
                    else
                    {
                        _frame = _score[18] + _total;
                    }
                }
                //前フレームの2投目がスペアだった場合
                else if (cmb9_2.Text == "/")
                {
                    _frame = _score[18] + _total;
                    lbl9_sum.Text = _frame.ToString();
                }
                //どれでもなかった場合、10フレーム目の合計値を蓄積
                else
                {
                    _frame = _score[18] + _total;
                }
            }
            //前フレームの1投目がストライクだった場合
            else if (cmb9_1.Text == "X")
            {
                cmb10_2.Enabled = true;

                //前々フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                if (cmb8_1.Text == "X")
                {
                    int.TryParse(lbl7_sum.Text, out _total);

                    //ターキー・ダブルボーナス点追加処理
                    Bonus1(18, lbl8_sum);
                }
            }
            //前フレームの2投目がスペアだった場合、ボーナス点を追加
            else if (cmb9_2.Text == "/")
            {
                cmb10_2.Enabled = true;

                _frame = _score[18] + _total;
                lbl9_sum.Text = _frame.ToString();
            }
            //どれでもなかった場合
            else
            {
                cmb10_2.Enabled = true;
            }
        }

        /// <summary>
        /// コンボボックス10_2選択時
        /// </summary>
        private void cmb10_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[19] = Point(cmb10_2.Text, 18);

            cmb10_3.Enabled = false;
            cmb10_3.Text = "";

            //1投目がストライクだった場合、もう一度投げられる
            if(cmb10_1.Text == "X")
            {
                //スコア代入処理(1投目の添え字は適当)
                _score[19] = Point(cmb10_2.Text, 0);

                lbl10_sum.Text = "";
                cmb10_3.Enabled = true;

                //2投目がストライクだった場合
                if (cmb10_2.Text == "X")
                {
                    //1投目がストライクだった場合
                    if (cmb10_1.Text == "X")
                    {
                        //前フレームの1投目がストライクだった場合、ターキーのボーナス点を追加
                        if (cmb9_1.Text == "X")
                        {
                            int.TryParse(lbl8_sum.Text, out _total);
                            
                            _total = _score[16] + _score[18] + _score[19] + _frame;
                            lbl9_sum.Text = _total.ToString();
                            
                            _total = _score[18] + _score[19] + _total;
                        }
                        //前フレームの1投目がストライクではなかった場合、10フレーム目の合計値を蓄積(ダブル)
                        else
                        {
                            _frame = _score[19] + _total;
                        }
                    }
                    //どれでもなかった場合、10フレーム目の合計値を蓄積
                    else
                    {
                        _frame = _score[19] + _total;
                    }
                }
                //1投目がストライクだった場合
                else if (cmb10_1.Text == "X")
                {
                    cmb10_3.Enabled = true;

                    //前フレームの1投目がストライクだった場合、ダブルのボーナス点を追加
                    if (cmb9_1.Text == "X")
                    {
                        int.TryParse(lbl8_sum.Text, out _total);

                        //ターキー・ダブルボーナス点追加処理
                        Bonus1(19, lbl9_sum);
                    }
                }
            }
            //1投目がストライクではなかった場合
            else
            {
                //前フレームの1投目がストライクだった場合
                if (cmb9_1.Text == "X")
                {
                    //ダブル・シングルボーナス点追加処理
                    Bonus2(19, lbl9_sum);
                }
                //前フレームの1投目がストライクではなかった場合、そのままスコアを追加
                else
                {
                    int.TryParse(lbl9_sum.Text, out _frame);
                    _total = _score[18] + _score[19] + _frame;
                }

                //2投目がスペアではなかった場合、合計値を印字
                if (cmb10_2.Text != "/")
                {
                    lbl10_sum.Text = _total.ToString();
                }
                //2投目がスペアだった場合、もう一回投げられる
                else
                {
                    cmb10_3.Enabled = true;
                    cmb10_3.Text = "";
                    lbl10_sum.Text = "";
                }
            }
        }

        /// <summary>
        /// コンボボックス10_3選択時
        /// </summary>
        private void cmb10_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //スコア代入処理
            _score[20] = Point(cmb10_3.Text, 19);

            //3投目がストライクだった場合
            if (cmb10_3.Text == "X")
            {
                //2投目がストライクだった場合
                if(cmb10_2.Text == "X")
                {
                    //1投目がストライクだった場合
                    if(cmb10_1.Text == "X")
                    {
                        int.TryParse(lbl9_sum.Text, out _total);

                        _total = (_score[18] + _score[19] + _score[20]) * 2 + _frame;

                        lbl10_sum.Text = _total.ToString();
                    }
                }
            }
            //1投目がストライクだった場合
            else if(cmb10_1.Text == "X")
            {
                _total = _score[19] + _score[20] + _frame;
                lbl10_sum.Text = _total.ToString();
            }
            else if(cmb10_2.Text == "/")
            {
                _total = _score[18] + _score[19] + _score[20] + _score[20] + _frame;
                lbl10_sum.Text = _total.ToString();
            }
        }

        /// <summary>
        /// コンボボックス【月】選択時
        /// </summary>
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //コンボボックス追加処理(日)
            Addday();
        }

        /// <summary>
        /// 「登録」ボタン押下時
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //どれか1フレームに空白があった場合
            if (lbl1_sum.Text == "" ||
                lbl2_sum.Text == "" ||
                lbl3_sum.Text == "" ||
                lbl4_sum.Text == "" ||
                lbl5_sum.Text == "" ||
                lbl6_sum.Text == "" ||
                lbl7_sum.Text == "" ||
                lbl8_sum.Text == "" ||
                lbl9_sum.Text == "" ||
                lbl10_sum.Text == "")
            {
                MessageBox.Show("スコアが正しく入力されていません。\n" +
                                "再度確かめてから登録ボタンを押してください。",
                                "スコア入力エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            //登録日が入力されていなかった場合
            else if(cmbYear.Text == ""  ||
                    cmbMonth.Text == "" ||
                    cmbDay.Text == "")
            {
                MessageBox.Show("登録日が正しく入力されていません。\n" +
                                "再度確かめてから登録ボタンを押してください。",
                                "登録日入力エラー",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            //全て正しく入力されていた場合
            else
            {
                DialogResult result = MessageBox.Show("スコアを登録します。\nよろしいですか？",
                                                      "スコア登録",
                                                      MessageBoxButtons.OKCancel,
                                                      MessageBoxIcon.Exclamation);

                if (result == DialogResult.OK)
                {
                    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Bowling.mdf;Integrated Security=True;Connect Timeout=30";
                    
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            //データベースの接続開始
                            connection.Open();

                            using (var transaction = connection.BeginTransaction())
                            using (var command = new SqlCommand() { Connection = connection, Transaction = transaction })
                            {
                                try
                                {
                                    //実行するSQLの準備
                                    command.CommandText = @"INSERT INTO dbo.Bowling_Table
                                                            (年,
                                                             月,
                                                             日,
                                                             Frame1_1,
                                                             Frame1_2,
                                                             Frame2_1,
                                                             Frame2_2,
                                                             Frame3_1,
                                                             Frame3_2,
                                                             Frame4_1,
                                                             Frame4_2,
                                                             Frame5_1,
                                                             Frame5_2,
                                                             Frame6_1,
                                                             Frame6_2,
                                                             Frame7_1,
                                                             Frame7_2,
                                                             Frame8_1,
                                                             Frame8_2,
                                                             Frame9_1,
                                                             Frame9_2,
                                                             Frame10_1,
                                                             Frame10_2,
                                                             Frame10_3,
                                                             Total)
                                                             VALUES
                                                            (@DataYear,
                                                             @DataMonth,
                                                             @DataDay,
                                                             @1_1,
                                                             @1_2,
                                                             @2_1,
                                                             @2_2,
                                                             @3_1,
                                                             @3_2,
                                                             @4_1,
                                                             @4_2,
                                                             @5_1,
                                                             @5_2,
                                                             @6_1,
                                                             @6_2,
                                                             @7_1,
                                                             @7_2,
                                                             @8_1,
                                                             @8_2,
                                                             @9_1,
                                                             @9_2,
                                                             @10_1,
                                                             @10_2,
                                                             @10_3,
                                                             @total)";
                                    
                                    command.Parameters.Add(new SqlParameter("@DataYear", cmbYear.Text));
                                    command.Parameters.Add(new SqlParameter("@DataMonth", cmbMonth.Text));
                                    command.Parameters.Add(new SqlParameter("@DataDay", cmbDay.Text));
                                    command.Parameters.Add(new SqlParameter("@1_1", cmb1_1.Text));
                                    command.Parameters.Add(new SqlParameter("@1_2", cmb1_2.Text));
                                    command.Parameters.Add(new SqlParameter("@2_1", cmb2_1.Text));
                                    command.Parameters.Add(new SqlParameter("@2_2", cmb2_2.Text));
                                    command.Parameters.Add(new SqlParameter("@3_1", cmb3_1.Text));
                                    command.Parameters.Add(new SqlParameter("@3_2", cmb3_2.Text));
                                    command.Parameters.Add(new SqlParameter("@4_1", cmb4_1.Text));
                                    command.Parameters.Add(new SqlParameter("@4_2", cmb4_2.Text));
                                    command.Parameters.Add(new SqlParameter("@5_1", cmb5_1.Text));
                                    command.Parameters.Add(new SqlParameter("@5_2", cmb5_2.Text));
                                    command.Parameters.Add(new SqlParameter("@6_1", cmb6_1.Text));
                                    command.Parameters.Add(new SqlParameter("@6_2", cmb6_2.Text));
                                    command.Parameters.Add(new SqlParameter("@7_1", cmb7_1.Text));
                                    command.Parameters.Add(new SqlParameter("@7_2", cmb7_2.Text));
                                    command.Parameters.Add(new SqlParameter("@8_1", cmb8_1.Text));
                                    command.Parameters.Add(new SqlParameter("@8_2", cmb8_2.Text));
                                    command.Parameters.Add(new SqlParameter("@9_1", cmb9_1.Text));
                                    command.Parameters.Add(new SqlParameter("@9_2", cmb9_2.Text));
                                    command.Parameters.Add(new SqlParameter("@10_1", cmb10_1.Text));
                                    command.Parameters.Add(new SqlParameter("@10_2", cmb10_2.Text));
                                    command.Parameters.Add(new SqlParameter("@10_3", cmb10_3.Text));
                                    command.Parameters.Add(new SqlParameter("@total", lbl10_sum.Text));

                                    //SQLの実行
                                    command.ExecuteNonQuery();

                                    MessageBox.Show("登録が完了しました。",
                                                    "登録完了！！",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);

                                    //空白処理
                                    Reset();
                                }
                                catch (Exception exception)
                                {
                                    MessageBox.Show(exception.Message,
                                                    "エラー",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Error);

                                    // ロールバック
                                    transaction.Rollback();
                                }
                                finally
                                {
                                    // コミット
                                    transaction.Commit();
                                }
                            }
                        }
                        finally
                        {
                            // データベースの接続終了
                            connection.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 「リセット」ボタン押下時
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            //空白処理
            Reset();
        }

        /// <summary>
        /// 「一覧」ボタン押下時
        /// </summary>
        private void btnList_Click(object sender, EventArgs e)
        {
            //スコア一覧の画面を表示
            Form2 frm = new Form2();
            frm.Show();
        }

        /// <summary>
        /// 「終了」ボタン押下時
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            //アプリケーションを閉じる
            this.Close();
        }

        /// <summary>
        /// 起動時時計が動くイベント
        /// </summary>
        private void timTime_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            //時刻を表示
            lblTime.Text = dt.ToString("yyyy年MM月dd日 HH時mm分ss秒");
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


            //コンボボックス追加処理(年)
            Addyear();

            //コンボボックス追加処理(月)
            Addmonth();

            //コンボボックス追加処理(日)
            Addday();
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
        /// コンボボックス追加処理(年)
        /// </summary>
        private void Addyear()
        {
            int i = 2010;

            DateTime dt = DateTime.Now;
            while (dt.Year >= i)
            {
                cmbYear.Items.Add(i);
                i++;
            }
        }

        /// <summary>
        /// コンボボックス追加処理(月)
        /// </summary>
        private void Addmonth()
        {
            int i = 1;

            while(i <= 12)
            {
                cmbMonth.Items.Add(i);
                i++;
            }
        }

        /// <summary>
        /// コンボボックス追加処理(日)
        /// </summary>
        private void Addday()
        {
            //選択時、コンボボックスの中身を消去する
            cmbDay.Items.Clear();

            int i = 1;

            while(i <= 31)
            {
                cmbDay.Items.Add(i);
                i++;
                
                if(cmbMonth.Text == "2" && i == 30)
                {
                    break;
                }
                else if((cmbMonth.Text == "4"  ||
                         cmbMonth.Text == "6"  ||
                         cmbMonth.Text == "9"  ||
                         cmbMonth.Text == "11")&&
                         i == 31)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// スコア代入処理
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
                int.TryParse(str, out int num);
                return num;
            }
        }

        /// <summary>
        /// ターキー・ダブルボーナス点追加処理(1投目)
        /// </summary>
        /// <param name="i">スコア配列の添え字</param>
        /// <param name="lbl">現在フレームの合計値</param>
        private void Bonus1(int i, Label lbl)
        {
            _frame = _score[i-4] + _score[i-2] + _score[i] + _total;
            lbl.Text = _frame.ToString();
        }

        /// <summary>
        /// ダブル・シングルボーナス点追加処理(2投目)
        /// </summary>
        /// <param name="i">スコア配列の添え字</param>
        /// <param name="lbl">現在フレームの合計値</param>
        private void Bonus2(int i, Label lbl)
        {
            //前フレームの合計値
            _total = _score[i-3] + _score[i-1] + _score[i] + _frame;
            lbl.Text = _total.ToString();

            //現在フレームの合計値
            _total = _score[i-1] + _score[i] + _total;
        }

        /// <summary>
        /// 空白処理
        /// </summary>
        private void Reset()
        {
            cmb1_1.SelectedIndex = -1;
            cmb1_2.SelectedIndex = -1;
            cmb2_1.SelectedIndex = -1;
            cmb2_2.SelectedIndex = -1;
            cmb3_1.SelectedIndex = -1;
            cmb3_2.SelectedIndex = -1;
            cmb4_1.SelectedIndex = -1;
            cmb4_2.SelectedIndex = -1;
            cmb5_1.SelectedIndex = -1;
            cmb5_2.SelectedIndex = -1;
            cmb6_1.SelectedIndex = -1;
            cmb6_2.SelectedIndex = -1;
            cmb7_1.SelectedIndex = -1;
            cmb7_2.SelectedIndex = -1;
            cmb8_1.SelectedIndex = -1;
            cmb8_2.SelectedIndex = -1;
            cmb9_1.SelectedIndex = -1;
            cmb9_2.SelectedIndex = -1;
            cmb10_1.SelectedIndex = -1;
            cmb10_2.SelectedIndex = -1;
            cmb10_3.SelectedIndex = -1;

            lbl1_sum.Text = "";
            lbl2_sum.Text = "";
            lbl3_sum.Text = "";
            lbl4_sum.Text = "";
            lbl5_sum.Text = "";
            lbl6_sum.Text = "";
            lbl7_sum.Text = "";
            lbl8_sum.Text = "";
            lbl9_sum.Text = "";
            lbl10_sum.Text = "";

            cmbYear.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            cmbDay.SelectedIndex = -1;

            cmb1_2.Enabled = false;
            cmb2_2.Enabled = false;
            cmb3_2.Enabled = false;
            cmb4_2.Enabled = false;
            cmb5_2.Enabled = false;
            cmb6_2.Enabled = false;
            cmb7_2.Enabled = false;
            cmb8_2.Enabled = false;
            cmb9_2.Enabled = false;
            cmb10_2.Enabled = false;
            cmb10_3.Enabled = false;
        }

        #endregion
    }
}
