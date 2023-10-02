namespace GreedyAlgorythms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int finalSum = 18;
            int currentSum = 0;
            int[] coins = { 10, 10, 5, 5, 2, 2, 1, 1 };

            Queue<int> resultCoins = new Queue<int>();

            // Следващия слайд
            for (int i = 0; i < coins.Length; i++)
            {
                if (currentSum + coins[i] > finalSum) continue;

                currentSum += coins[i];
                resultCoins.Enqueue(coins[i]);
                if (currentSum == finalSum)
                {

                    // Sum Found
                    Console.WriteLine("Sum found = " + currentSum);


                }
                else
                {
                    Console.WriteLine("Sum not Found = " + currentSum);

                }
            }
            
            
        }
    }
}