# Calculate the summation of powers

공군기훈단 훈련소에서 동기가 수학적귀납법 없이 자연수 거듭제곱합을 구하는 것에 대해 질문했다.

당시 점심을 먹으러 가면서 머릿속으로 생각하다보니 괜찮은 풀이가 떠올랐고, c# 공부해볼겸 이걸 계산하는 cs 코드를 만들었다.

# Key idea

1의 제곱부터 n의 제곱까지의 합을 생각해보면 규칙성이 하나 있다.

$1, 4, 9, 16, ...$

더해지는 수들의 간격이 연속된 홀수이다. $n^2-(n-1)^2=2n-1$

이를 이용하여 수를 재정렬하면 다음과 같다.

$
\begin{aligned}
1, 1, 1, 1, &...\\
3, 3, 3, &...\\
5, 5, &...\\
7, &...\\
\end{aligned}
$

같은 열끼리 더하면 처음의 합인 $1, 4, 9, 16, ...$ 과 같다.

반대로 같은 행끼리 더한다면 최종적인 합은 그대로이지만 식은 달라진다.

$I=1+4+9+16+...+n^2$

이라고 하자.

이때 $i$번쨰 행의 합은 다음과 같다.

$(n-i+1)(2i-1)$

이를 이용해 $I$ 를 다시 쓰면 다음과 같다.

$
\begin{aligned}
I&=\displaystyle\sum_{i=1}^{n}{(n-i+1)(2i-1)}\\
&=\displaystyle\sum_{i=1}^{n}{(-2i^2+(2n+3)i-n-1)}\\
&=-2I+(2n+3)\displaystyle\sum_{i=1}^{n}{i}-n(n+1)\\
\end{aligned}
$

$
\begin{aligned}
3I&=(2n+3)\displaystyle\sum_{i=1}^{n}{i}-n(n+1)\\
&={n(n+1)(2n+1)\over2}
\end{aligned}
$

$\therefore I={n(n+1)(2n+1)\over6}$

# Generalization

$I_k=\displaystyle\sum_{i=1}^{n}{i^k}$

$D_k(i):=i^k-(i-1)^k$

다음 나열된 수들의 합과 $I_k$ 는 같다.

$
\begin{aligned}
D_k(1), D_k(1), D_k(1), D_k(1), &...\\
D_k(2), D_k(2), D_k(2), &...\\
D_k(3), D_k(3), &...\\
D_k(4), &...\\
\end{aligned}
$

$I_k=\displaystyle\sum_{i=1}^{n}{(n-i+1)D_k(i)}$

$D_k(i)$ 는 binomial coefficients 로 표현

정리하면

$I_k={1\over(k+1)}\left((n+1)(-1)^{k+1}I_0+\displaystyle\sum_{i=1}^{n}{\left((n+i)\begin{pmatrix}k\\i\\ \end{pmatrix}+\begin{pmatrix}k\\i-1\\ \end{pmatrix}I_i\right)}\right)$

위와 같은 recurrence relation 을 바탕으로 dynamic programming 을 이용하면 $I_k$ 의 모든 계수를 계산할 수 있다.

# Optimization

C# 문법에 익숙치 않아 아직 시간/공간적 최적화는 진행하지 못했다.

FFT, deep copy 등을 활용하면 효율적으로 계산될 듯하다

자료형도 int 가 아닌 ulong 으로 바꾸어 더 큰 범위까지 계산되도록 바꿀 필요가 있다.

