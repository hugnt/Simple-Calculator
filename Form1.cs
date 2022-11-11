using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STool
{
    public partial class Form1 : Form
    {

        public bool outPut = false;
        public double result,Ans =0;
        public bool check_error = false;
        
        public Form1()
        {
            InitializeComponent();
        }
      
        public string Control_Input(string str_input)
        {
            //Dau --;+-;-+;x-;%-;
            //string fixed_input = "";
            int l = str_input.Length;
            int i = 0;
            if(str_input.Contains("--"))
            {
                str_input.Replace("--", "+");
            }
            string s1 = str_input.Substring(i, 1), s2 = "",stack="";
            bool check_inloop = false;
            
           while(i<l)
            {
                if(check_inloop==true)
                {
                    i = 0;
                    check_inloop = false;
                }
               
                    s1 = str_input.Substring(i, 1); s2 = "";
                    while (s1 == "+" || s1 == "-" || s1 == "x" || s1 == "%")
                    {
                    check_inloop = false;
                    s2 = s1;
                        i++;
                        if (i == l) break;
                        s1 = str_input.Substring(i, 1);
                        if (s2 == s1)
                        {
                       
                            if (s1 == "+")
                            {
                                str_input = str_input.Replace("++", "+"); l--;  check_inloop = true; break;

                        }
                            if (s1 == "-")
                            {
                                str_input = str_input.Replace("--", "+"); l--;  check_inloop = true; break;

                        }

                        }
                        if (s2 == "+" && s1 == "-")
                        {
                            str_input = str_input.Replace("+-", "-"); l--; check_inloop = true; break;
                    }
                        if (s2 == "-" && s1 == "+")
                        {
                            str_input = str_input.Replace("-+", "-"); l--;  check_inloop = true; break;
                    }
                        if (s2 == "x" && s1 == "+")
                        {
                            str_input = str_input.Replace("x+", "x"); l--; check_inloop = true; break;
                    }
                        if (s2 == "%" && s1 == "+")
                        {
                            str_input = str_input.Replace("%+", "%"); l--; check_inloop = true; break;
                    }
                        if (s2 == "x" && s1 == "-")
                        {
                        check_inloop = true;

                        stack = str_input.Remove(i, 1);
                            int j = i;
                            string s4 = stack.Substring(j - 1, 1);
                            while (s4 != "+" && s4 != "-" && j > 0)
                            {
                                j--;
                                if (j == 0) break;
                                s4 = stack.Substring(j - 1, 1);
                            }
                            str_input = stack.Insert(j, "-");
                        i = 0;
                            break;
                        }
                        if (s2 == "%" && s1 == "-")
                        {
                        check_inloop = true;

                        stack = str_input.Remove(i, 1);
                        int j = i;
                        string s4 = stack.Substring(j - 1, 1);
                        while (s4 != "+" && s4 != "-" && j > 0)
                        {
                            j--;
                            if (j == 0) break;
                            s4 = stack.Substring(j - 1, 1);
                        }
                        str_input = stack.Insert(j, "-");
                        i = 0;
                        break;

                    }
                }
                if(check_inloop==false)
                {
                    i++;
                }


            }
            

            return str_input;
        }
        public string Check_Group(string str_input)
        {
            int i = 0, l = str_input.Length;
            string output1 = "";
            while (i < l)
            {
                if (str_input.Contains("("))
                {
                    int count = 0;
                    string s1 = str_input.Substring(i, 1);
                    while (s1 != "(" && i < l)
                    {
                        i++;
                        s1 = str_input.Substring(i, 1);
                    }
                    s1 = "";
                    while (s1 != ")" && i < l)
                    {
                        output1 += s1;
                        i++;
                        count++;
                        s1 = str_input.Substring(i, 1);
                    }
                   
                    str_input = str_input.Replace("(" + output1 + ")", Solve(output1).ToString());
                    l = str_input.Length; i = 0;

                }
                i++;
            }    
            return str_input;
        }
        public bool Check_Error(string str_input)
        {
            //Loi xx;%%;-x;-%,)(,(((..)),%0,----,4 dau tro len,....,so<=dau
            int l = str_input.Length, i = 0, count1 = 0, count2 = 0,count_doc=0;
            string s1 = str_input.Substring(i, 1);
          bool check_gr=false;
            if(str_input.Contains("xx")|| str_input.Contains("%%")||str_input.Contains("-x") || str_input.Contains("+x") || str_input.Contains("-%") || str_input.Contains("+%") || str_input.Contains("%0") || str_input.Contains("..") || str_input.Contains("%--") || str_input.Contains("%+-") || str_input.Contains("%++") || str_input.Contains("x++") || str_input.Contains("x+-") || str_input.Contains("%-+") || str_input.Contains("x--") || str_input.Contains("x-+") || str_input.Contains("x%") || str_input.Contains("%x") || str_input.Contains("++++") || str_input.Contains("----"))
            {
                return true;
            }
            if (str_input.Contains("(") ||str_input.Contains(")"))
         {
                while (i < l)
                {
                    if (s1 == "(")
                    {
                        check_gr = true;
                    }
                    if (s1 == ")" && check_gr == false)
                    {
                        return true;
                    }
                    i++;
                    if (i == l)
                    { break; }
                    s1 = str_input.Substring(i, 1);
                }
                i = 0;
                s1 = str_input.Substring(i, 1);
                while (i < l)
                {
                    if (s1 == "(")
                    {
                        count1++;
                    }
                    if (s1 == ")")
                    {
                        count2++;
                    }
                    i++;
                    if (i == l)
                    {
                        break;
                    }
                    s1 = str_input.Substring(i, 1);

                }
                if (count1 != count2)
                {
                    return true;
                }
            }
            i = 0;
            s1 = str_input.Substring(i, 1);

            while (i<l)
            {
                s1 = str_input.Substring(i, 1);
                while (s1 != "+" && s1 != "-" && s1 != "x" && s1 != "%" && i<l)
                {
                    if(s1==".")
                    {
                        count_doc++;
                    }
                    i++;
                    if (i == l)
                    { break; }
                    s1 = str_input.Substring(i, 1);
                }
                i++;
                if (i == l) break;
                if (count_doc >= 2)
                {
                    return true;
                }
                else
                    break;
            }
           
            s1 = str_input.Substring(l-1, 1);
            if(s1=="+"||s1=="-" || s1 == "x" || s1 == "%")
            {
                return true;
            }

            return false;
           
        }
        public double Solve_MD(string MD_input)
        {
            double MD;
            int l = MD_input.Length,i=0;
            string s1 = MD_input.Substring(i,1),s2="",opr_MD;
            while(s1!="x"&&s1!="%"&&i<l)
            {
                s2 = s2 + s1;
                i++;
                if (i == l) break;
                s1 = MD_input.Substring(i,1);
            }
            MD = Double.Parse(s2);
          
            while (i<l)
            {
                opr_MD = s1;s2 = "";
                i++;
                s1 = MD_input.Substring(i,1);
                while (s1!="x"&&s1!="%"&&i<l)
                {
                    s2 = s2 + s1;
                    i++;
                    if (i == l) break;
                    s1= MD_input.Substring(i, 1);
                }
                if(opr_MD=="x")
                {
                    MD *= Double.Parse(s2);
                    continue;
                }
                else if (opr_MD == "%")
                {
                    MD /= Double.Parse(s2);

                    continue;
                }
            }
            return MD;
            
        }
        public double Solve(string str_input)
        {
            str_input=Control_Input(str_input);
            int l = str_input.Length;
            result = 0;
            while(l>0)
            {
                string s1 = str_input.Substring(l - 1, 1);
                string s2 = "",opr="";
                while(s1!="+"&& s1 != "-" && s1 != "x" && s1 != "%"&&l>0)
                {
                    s2 = s1 + s2;
                    l--;
                    if (l == 0) break;
                    s1 = str_input.Substring(l - 1, 1);
                }
                if (l == 0)
                {
                    opr = "";
                }
                else
                { opr = s1; }
                switch(opr)
                {
                    case "":
                        result += Double.Parse(s2);
                        break;
              
                    case "+":
                        result += Double.Parse(s2);
                        l--;
                        continue;
                    case "-":
                        result -= Double.Parse(s2);
                        l--;
                        continue;
                }
                if(opr=="x"||opr=="%")
                {
                    while(s1!="+"&&s1!="-"&&l>0)
                    {
                        s2 = s1 + s2;
                        l--;
                        if (l == 0) break;
                        s1 = str_input.Substring(l - 1, 1);
                    }
                    if(l==0)
                    {
                        result += Solve_MD(s2);
                    }
                    if(s1=="+")
                    {
                        result += Solve_MD(s2);
                        l--;
                    }
                    else if (s1 == "-")
                    {
                        result -= Solve_MD(s2);
                        l--;
                    }
                }

            }
            return result;
            
        }
        private void InputNumber(object sender, EventArgs e)
        {
            Button n = (Button)sender;
            if (outPut == true)
            {
                screen.Text = "";
                screen.Text += n.Text;
                outPut = false;
            }
            else
            {
                if (n.Text == "π")
                    screen.Text += "3.14";
                else
                    screen.Text += n.Text;
            }
        }

        private void InputOperator(object sender, EventArgs e)
        {
            Button opr = (Button)sender;
     
            if (outPut == true)
            {
                screen.Text =Ans.ToString();
                screen.Text+= opr.Text;
                outPut = false;
            }
            else
            {
      
                screen.Text += opr.Text;
            }
        }

        private void Answer(object sender, EventArgs e)
        {
            if(outPut==true)
            {
                screen.Text ="";
                outPut = false;
            }
            screen.Text += Ans.ToString();
        }

        private void Result(object sender, EventArgs e)
        {
            if (Check_Error(screen.Text) == false)
            {
                resultSceen.Text = Solve(Control_Input(Check_Group(screen.Text))).ToString();
                Ans = Double.Parse(resultSceen.Text);
                outPut = true;
            }
            else
            {
                screen.Text = "Your algorithms were wrong, please check it!";
                resultSceen.Text = "Error";
                check_error = true;
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            screen.Text = "";
            resultSceen.Text = "";
        }

        private void Group_M(object sender, EventArgs e)
        {
            Button group = (Button)sender;
            screen.Text += group.Text;
        }

        private void Delete(object sender, EventArgs e)
        { 
            if(check_error ==true)
            {
                screen.Text = "";
                resultSceen.Text = "";
            }
            if(screen.Text.Length>0)
            {
                screen.Text = screen.Text.Remove(screen.Text.Length-1, 1);
                return;
            }
           
        }
    }
}
