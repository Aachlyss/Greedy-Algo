int[] weights = { 7 };
int[] values = { 42 };
int maxWeight = 10;

int[] dp = new int[maxWeight + 1];

for (int i = 0; i < weights.Length; i++)
{
    for (int j = maxWeight; j >= weights[i]; j--)
    {

        dp[j] = Math.Max(dp[j], dp[j - weights[i]] + values[i]);
    }
}
int maxTotalValue = dp[maxWeight];
Console.WriteLine($"Максималната обща цена: {maxTotalValue}");