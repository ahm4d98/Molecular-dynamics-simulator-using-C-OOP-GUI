using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace hatem4
{

    public partial class Form1 : Form
    {
        int number;

        cell root = null;
        cell currentc = null;
        Timer timer = new Timer();
        bool is_On = false;
        int n;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }




        private void insertcell(ref cell list, cell a)
        {
            if (list == null)
            { list = a; }
            else
            {
                cell cur = list;
                while ((cur.getNext() != null))
                {
                    cur.setNext(a);
                    cur = cur.getNext();


                }
            }
        }

        private void updateVelocity(object sender, EventArgs e)
        {
            cell cellPointer = root;
            Atom atomPointer;

            Random rand = new Random();
            double change;
            int range = 2 - (-1);

            while (cellPointer != null)
            {
                atomPointer = cellPointer.getAtom();
                while (atomPointer != null)
                {
                    //  rand * range + min
                    change = rand.NextDouble() * range + (-1);
                    double newVelocity = atomPointer.get_v() + change;
                    atomPointer.set_v(newVelocity);
                    double newVX = atomPointer.get_vx() + change;
                    atomPointer.set_vx(newVX);
                    double newVY = atomPointer.get_vy() + change;
                    atomPointer.set_vy(newVY);
                    double newVZ = atomPointer.get_vz() + change;
                    atomPointer.set_vz(newVZ);
                    //update x,y,z
                    double newx = atomPointer.get_x() + atomPointer.get_vx() ;
                    atomPointer.set_x(newx);
                    double newy = atomPointer.get_x() + atomPointer.get_vy() ;
                    atomPointer.set_y(newy);
                    double newz = atomPointer.get_x() + atomPointer.get_vz() ;
                    atomPointer.set_z(newz);


                    atomPointer = atomPointer.getNext();
                }
                cellPointer = cellPointer.getNext();
            }
            display();
        }
         void distance(Atom atom1, Atom atom2)
        {
            listView1.Items.Add("calculat distance between atoms");
            Atom a1 = atom1;
            Atom a2 = atom2;
            while (a1 != null)
            {
                a2 = a1.getNext();
                while (a2 != null)
                {

                    double dist = Math.Sqrt(Math.Pow(a1.get_x() - a2.get_x(), 2) + Math.Pow(a1.get_y() - a2.get_y(), 2) + Math.Pow(a1.get_z() - a2.get_z(), 2));
                    dist -= a1.get_r() + a2.get_r();
                    if (dist < a1.get_r() + a2.get_r())
                    {
                        listView1.Items.Add("Atom " + a1.get_n() + " make collision with" + " Atom " + a2.get_n());
                    }
                    a2 = a2.next;
                }
                a1 = a1.next;
            }
        }


        private void display()
        {
            cell cel = root;
            Atom att;
            while (cel != null)
            {
                att = cel.getAtom();
                listView1.Items.Add("cell " + cel.getNum());

                while(att != null)
                {

                    listView1.Items.Add(" x = " + att.get_x() + ", y = " + att.get_y() +", z = " + att.get_z() + ", v = " + att.get_v().ToString("n2"));
                    att = att.getNext();



                }
                distance(cel.getAtom(), cel.getAtom());

                cel = cel.getNext();
            }


        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            cell newcell = new cell(number);
            currentc = newcell;
            number++;
            insertcell(ref root, newcell);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            display();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Atom newAtom;
            double x, y, z , v;
            double xv,yv,zv;
            double r;
            x = Convert.ToDouble(textBox1.Text);
            y = Convert.ToDouble(textBox2.Text);
            z= Convert.ToDouble(textBox3.Text);
            v= Convert.ToDouble(textBox7.Text);
            xv = Convert.ToDouble(textBox4.Text);
            yv = Convert.ToDouble(textBox5.Text);
            zv = Convert.ToDouble(textBox6.Text);
            r = Convert.ToDouble(textBox7.Text);
            newAtom = new Atom(x, y, z, v , xv, yv, zv,r,n);

            currentc.insertAtom(newAtom);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //start
            timer.Tick += new EventHandler(updateVelocity);
            timer.Interval = 6000;
            timer.Start();
            is_On = true;
            while (is_On) // timer is on
            {
                Application.DoEvents();
            }

        }

       

        class Atom
        {
            public int n;
            double x, y, z , xv,yv,zv,r;
            double velocity;
            public Atom next = null;
            public Atom() { }
            public Atom(double x, double y, double z, double velocity , double xv , double yv , double zv, double r,int n)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.xv = xv;
                this.yv = yv;
                this.zv = zv;
                this.velocity = velocity;
                this.r = r;
                this.n = n;
            }
            public void setNext(Atom a)
            { next = a; }
            public double get_x()
            { return x; }
            public int get_n()
            { return n; }
            public void set_x(double x1)
            { x=x1; }
            public double get_y()
            { return y; }
            public void set_y(double y1)
            { y = y1; }
            public double get_z()
            { return z; }
            public void set_z(double z1)
            { z = z1; }
            public double get_vx()
            { return xv; }
            public double get_vy()
            { return yv; }
            public double get_vz()
            { return zv; }
            public double get_r()
            { return r; }
            public void set_vx(double xv1)
            { xv=xv1; }
            public void set_vy(double yv1)
            { yv = yv1; }
            public void set_vz(double zv1)
            { zv = zv1; }


            public double get_yv()
            { return yv; }
            public double get_zv()
            { return zv; }
            public double get_v()
            { return velocity; }
            public void set_v(double v)
            { velocity = v; }
            public Atom getNext()
            { return next; }
        }
        class cell
        {
            int number;
            Atom atoms;
            cell next = null;
            public cell(int number) { this.number = number ; }
            public void setAtom(Atom list) { this.atoms = list; }
            public void setNext(cell c) { next = c; }
            public int getNum() { return number; }
            public Atom getAtom() { return atoms; }
            public cell getNext() { return next; }
            public void insertAtom(Atom newAtom)
            {
                if (atoms == null) atoms = newAtom;
                else
                {
                    Atom train = atoms;
                    do
                    {

                        train.setNext(newAtom);
                        train = train.getNext();

                    } while (train.getNext() != null);

                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (is_On)
            {
                is_On = false;
                timer.Stop();
                timer.Tick -= new EventHandler(updateVelocity);

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}