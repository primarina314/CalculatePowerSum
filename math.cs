using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Math
{
    class Functions
    {
		public static int[,] combinationDP = null;
		public static int size = 100;
        public static Polynomial[] powerDP = null;
        public static int Combination(int a, int b)
        {
			if(combinationDP == null)
			{	
				combinationDP = new int[100, 100];
			    for(int i=0;i<size;i++) for(int j=0;j<size;j++) combinationDP[i,j] = 0;
			}
			return getCombination(a, b);
        }
		public static int getCombination(int a, int b)
		{
			if(combinationDP[a, b] != 0) return combinationDP[a, b];
			return combinationDP[a, b] = (b==0 || a==b) ? 1 : getCombination(a-1, b-1) + getCombination(a-1, b);
		}
        public static int Permutation(int a, int b)
        {
            if(a<b) return 0;
            int res = 1;
            while(b-- > 0) res *= a--;
            return res;
        }
        public static int max(params int[] numbers)
        {
            int res = Int32.MinValue;
            foreach(int number in numbers) res = (res>number) ? res : number;
            return res;
        }
        public static int min(params int[] numbers)
        {
            int res = Int32.MaxValue;
            foreach(int number in numbers) res = (res<number) ? res : number;
            return res;
        }
        public static int pow(int a, int b)
        {
            if(b==0) return 1;
            if(b==1) return a;
			if(a==1) return 1;
            if(a==-1) return ((b&1) == 1) ? -1 : 1;

            int sqrt = pow(a, b>>1);
            if((b&1) == 1) return a*sqrt*sqrt;
            else return sqrt*sqrt;
        }


        public static Polynomial CalculatePowerSum(int n)
        {
            if(powerDP == null)
            {
                powerDP = new Polynomial[size];
                for(int i=0;i<size;i++)
                {
                    Fraction[] tmpfor = new Fraction[1]{new Fraction(0, 0)};
                    powerDP[i] = new Polynomial(tmpfor);
                }
                Fraction[] tmp = new Fraction[2]{new Fraction(0,1), new Fraction(1,1)};
                powerDP[0] = new Polynomial(tmp);
            }
            return getPowerSum(n);
        }
        public static Polynomial getPowerSum(int p)
        {
			if(powerDP[p].GetValid()) return powerDP[p];
			Fraction[] a = new Fraction[3]{new Fraction(0,1), new Fraction(pow(-1,p+1),1), new Fraction(pow(-1,p+1),1)};
            Fraction[] b = new Fraction[2]{new Fraction(1,1), new Fraction(1,1)};
			Fraction[] c = new Fraction[1]{new Fraction(1,1)};
			Polynomial res = new Polynomial(a);
			Polynomial nPlusOne = new Polynomial(b);
			Polynomial one = new Polynomial(c);
			for(int i=1;i<p;i++)
			{	
				res = new Polynomial(
					res + 
					(new Fraction(pow(-1, p-i+1), 1)) * (
					nPlusOne*(new Fraction(Combination(p, i),1)) + one*(new Fraction(Combination(p,i-1),1))
					) * getPowerSum(i)
					);
			}
			Fraction[] d = new Fraction[1]{new Fraction(1,p+1)};
			Polynomial oneOverPPlusOne = new Polynomial(d);
			
			return powerDP[p] = new Polynomial(res * oneOverPPlusOne);
        }

    }

    class Complex
    {
        public decimal real { get; set; }
        public decimal img { get; set; }

        public Complex(decimal real, decimal img=0M) { (this.real, this.img) = (real, img); }
        public Complex(string str)
        {
            if(Decimal.TryParse(str, out var result)) real = result;
            else real = 0M;
            img = 0M;
        }

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.real+c2.real, c1.img+c2.img);
        }
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.real * c2.real - c1.img*c2.img, c1.real * c2.img + c1.img * c2.real);
        }
        public void show()
        {
            Console.WriteLine($"{real} + i{img}");
        }
        
    }

    class Fraction
    {
        public int numerator { get; set; }
        public int denominator { get; set; }
        private bool valid;
		
		public bool GetValid() { return valid; }

        public Fraction(Fraction fraction)
        {
            (this.numerator, this.denominator) = (fraction.numerator, fraction.denominator);
            valid = this.denominator!=0;
        }
        public Fraction(int numerator, int denominator)
        {
            if(numerator==0 && denominator!=0) (this.numerator, this.denominator) = (0, 1);
            else if(denominator!=0)
            {
                int gcd = Gcd(numerator, denominator);
                (this.numerator, this.denominator) = (numerator/gcd, denominator/gcd);
				if(this.denominator < 0)
				{
					(this.numerator, this.denominator) = (-this.numerator, -this.denominator);
				}
            }
            valid = denominator!=0;
        }
        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            if(f1.numerator==0) return new Fraction(f2);
            if(f2.numerator==0) return new Fraction(f1);
			
			if(f1.denominator==0) Console.WriteLine("f1 div 0");
			else if(f2.denominator==0) Console.WriteLine("f2 div 0");

            int d = f1.Lcm(f1.denominator, f2.denominator);
            int time1 = d / f1.denominator;
            int time2 = d / f2.denominator;
            int n = f1.numerator * time1 + f2.numerator * time2;
			
            int gcd = (n!=0) ? f1.Gcd(n, d) : 1;

            return new Fraction(n/gcd, d/gcd);
        }

        public static Fraction operator* (Fraction f1, Fraction f2)
        {
            if(f1.numerator==0 || f2.numerator==0) return new Fraction(0, 1);

            int gcd1 = f1.Gcd(f1.numerator, f2.denominator);
            int gcd2 = f1.Gcd(f1.denominator, f2.numerator);

            return new Fraction(f1.numerator/gcd1 * f2.numerator/gcd2, f1.denominator/gcd2 * f2.denominator/gcd1);
        }
		

        public int Gcd(int a, int b)
        {
			a = (a>0) ? a : -a;
			b = (b>0) ? b : -b;
			
            int p = (a > b) ? a : b;
            int q = (a > b) ? b : a;
            while(p%q!=0) (p, q) = (q, p%q);
            return q;
        }
        public int Lcm(int a, int b)
        {
			a = (a>0) ? a : -a;
			b = (b>0) ? b : -b;
            return a / Gcd(a, b) * b;
        }
        public void show()
        {
            Console.WriteLine($"{numerator}/{denominator}");
        }
		public string ToString()
		{
			return $"{numerator}/{denominator}";
		}
    }

    class Polynomial
    {
        private int degree { get; }
        private Fraction[] coefficients;
        private bool valid;
        public bool GetValid() { return valid; }

        public Polynomial(Polynomial polynomial)
        {
            this.degree = polynomial.degree;
            this.valid = polynomial.valid;
            coefficients = new Fraction[this.degree+1];
            for(int i=0;i<this.coefficients.Length;i++)
            {
                this.coefficients[i] = new Fraction(polynomial[i]);
            }
        }
        public Polynomial(Fraction[] coefficients)
        {	
            this.degree = coefficients.Length-1;
            this.coefficients = new Fraction[this.degree+1];
            this.valid = true;
            for(int i=0;i<this.coefficients.Length;i++)
            {
                this.coefficients[i] = new Fraction(coefficients[i]);
				
                this.valid = this.valid && this.coefficients[i].GetValid();
            }
        }

        public static Polynomial operator +(Polynomial a, Polynomial b)
        {
            int deg = (a.degree > b.degree) ? a.degree : b.degree;
            Fraction[] coef = new Fraction[deg+1];
            for(int i=0;i<coef.Length;i++)
            {
                Fraction p, q;
                if(i>a.degree) p = new Fraction(0, 1);
                else p = new Fraction(a[i]);
                if(i>b.degree) q = new Fraction(0, 1);
                else q = new Fraction(b[i]);
				coef[i] = new Fraction(p+q);
            }
            return new Polynomial(coef);
        }
        
        public static Polynomial operator *(Polynomial a, Polynomial b)
        {
            int deg = a.degree + b.degree;
            Fraction[] coef = new Fraction[deg+1];
            // could use FFT
            // but adapt it later, not now
            
            for(int i=0;i<coef.Length;i++)
            {
                // deg: i
                int from = Functions.max(0, i-b.degree);
                int to = Functions.min(i, a.degree);
                Fraction res = new Fraction(0,1);
                for(int j=from;j<=to;j++)
                {
                    res = new Fraction(res + a[j] * b[i-j]);
                }
                coef[i] = new Fraction(res);
            }
            
            return new Polynomial(coef);
        }
		
		public static Polynomial operator *(Polynomial p, Fraction f)
        {
            Polynomial res = new Polynomial(p);
            for(int i=0;i<p.coefficients.Length;i++)
            {
                res.coefficients[i] = new Fraction(p.coefficients[i] * f);
            }
            return res;
        }
		
		public static Polynomial operator *(Fraction f, Polynomial p)
        {
            return p * f;
        }
        
        

        public Fraction this[int idx]
        {
            get
            {
                if(idx >= coefficients.Length) return new Fraction(0,0);
                return coefficients[idx];
            }
        }
        
        public void show()
        {
            for(int i=0;i<this.coefficients.Length;i++)
            {
                Console.Write(this.coefficients[i].ToString() + "x^" + i.ToString() + " ");
            }
            Console.WriteLine();
        }
    }

}
