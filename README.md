# Calculate the summation of powers

This c# code find the polynomial of power sum in the form of fractions. For instance, if input is 2, the result would be '1/3 x^2 + 1/2 x^1 + 1/6 x^0'

# Key idea

Considering the sqaure summation from 1 to n, there's a specific pattern below.

$$1, 4, 9, 16, \cdots$$

The difference(gap) between the consecutive two sqaures is equal to an odd number.
As the step goes on, the gap becomes the next odd number.(1,3,5,7,... and so on)
$n^2-(n-1)^2=2n-1$

Using this, rearrange the gaps like below.

$$\begin{aligned}
1, 1, 1, 1, &\cdots\\
3, 3, 3, &\cdots\\
5, 5, &\cdots\\
7, &\cdots\\
\end{aligned}$$


Sum among the elements in a same column is equal to the initial one, $1, 4, 9, 16, \cdots$, respectively.

On the constrast, sum among the elements in a same row has different form of expression.
But the total sum doesn't change(Sum of row sum = Sum of col sum)


Let
$$I=1+4+9+16+\cdots+n^2$$


Then, $i$th row's sum is below.

$$(n-i+1)(2i-1)$$

With this, rewrite $I$ below.

$$
\begin{aligned}
I&=\displaystyle\sum_{i=1}^{n}{(n-i+1)(2i-1)}\\
&=\displaystyle\sum_{i=1}^{n}{(-2i^2+(2n+3)i-n-1)}\\
&=-2I+(2n+3)\displaystyle\sum_{i=1}^{n}{i}-n(n+1)\\
\end{aligned}
$$

$$
\begin{aligned}
3I&=(2n+3)\displaystyle\sum_{i=1}^{n}{i}-n(n+1)\\
&={n(n+1)(2n+1)\over2}
\end{aligned}
$$

$$\therefore I={n(n+1)(2n+1)\over6}$$

# Generalization

$$I_k=\displaystyle\sum_{i=1}^{n}{i^k}$$

$$D_k(i):=i^k-(i-1)^k$$

다음 나열된 수들의 합과 $I_k$ 는 같다.

$$
\begin{aligned}
D_k(1), D_k(1), D_k(1), D_k(1), &\cdots\\
D_k(2), D_k(2), D_k(2), &...\\
D_k(3), D_k(3), &...\\
D_k(4), &...\\
\end{aligned}
$$

$$I_k=\displaystyle\sum_{i=1}^{n}{(n-i+1)D_k(i)}$$

$D_k(i)$ can be expressed with binomial coefficients

Rearranging the whole expressions below.

$$
\begin{aligned}
&I_k(n)={1\over(k+1)}\left((n+1)(-1)^{k+1}I_0+\displaystyle\sum_{i=1}^{n}{\left((n+i)
\begin{pmatrix} k\\
i\\ \end{pmatrix}
+\begin{pmatrix}k\\
i-1\\ \end{pmatrix} I_i\right)}\right)
\\
&where\quad I_0(n)=n
\end{aligned}
$$

Based on the recurrence relation, the all coefficients of $I_k$ can be calculated with dynamic programming.

# Optimization

C# 문법에 익숙치 않아 아직 시간/공간적 최적화는 진행하지 못했다.

FFT, deep copy 등을 활용하면 효율적으로 계산될 듯하다

자료형도 int 가 아닌 ulong 으로 바꾸어 더 큰 범위까지 계산되도록 바꿀 필요가 있다.
