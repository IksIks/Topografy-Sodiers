using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Топография_солдаты
{
	class Program
	{
		public static int[] fillingArray(int[] scoreAndNumber, int[] array, int vip = 0)
		{
			int symbol = scoreAndNumber[0];
			int numberOfSymbol = scoreAndNumber[1];
			if (scoreAndNumber.Length > 2 && symbol == 2)	// проверка на то, что в массиве есть 2 и в нем есть конкнретные
															// позиции для двойки
			{
				for (int i = 2; i < scoreAndNumber.Length; i++)
				{
					int position = scoreAndNumber[i];
					array[position - 1] = symbol;
				}
			}
			else
			{
				int execute = 0;        //число которое будет исключаться из списка после присвоения
				for (int i = 0; i < numberOfSymbol; i++)
				{
					int test = 0;
					while (true)
					{
						test = sortingRandom(vip, array.Length, execute);    //случайное число переданное из функции ниже
						if (array[test] == 0)
						{
							array[test] = symbol;
							break;
						}
						else execute = test;
					}
				}
			}
			return array;
		}
		/// <summary>
		/// случайная генерация чисел с учетом не повторения уже выданных чмсел
		/// </summary>
		/// <param name="range">диапазон максимально генерируемого числа</param>
		/// <param name="executable">массив использованных чисел</param>
		/// <returns></returns>
		public static int sortingRandom(int rangeMin, int rangeMax, params int[] executable)
		{
			Random r = new Random();
			int[] validValues = Enumerable.Range(0, rangeMax).Except(executable).ToArray();
			return validValues[r.Next(rangeMin, validValues.Length)];
		}
		public static string[] generetanigPrakticalAnswers(int score)
		{
			string[] answers = new string[460];
			double[] values = { 5, 4, 3, 2 };
			int next = 0;
			if ((score == 5) || (score == 4))           //
				for (int i = 0; i < values.Length - 1; i++)  //фильтр генерации оценок только из 5 4 3. Length -1 убирает
				{											 //2, чтоб не было оценок вида 5 5 5 2 5 итог 4
				                                           
					for (int j = 0; j < values.Length - 1; j++)
					{
						for (int k = 0; k < values.Length - 1; k++)
						{
							
							for (int l = 0; l < values.Length - 1; l++)
							{
								for (int m = 0; m < values.Length - 1; m++)
								{
									double average = (values[i] + values[j] + values[k] + values[l] + values[m]) / 5;
									double averageMath = Math.Round((values[i] + values[j] + values[k] + values[l] + values[m]) / 5);
									if (((averageMath - average) <= 0.3) && (averageMath == score))
									{
										answers[next] = ($"{values[i]} {values[j]} {values[k]} {values[l]} {values[m]}").ToString();
										//Console.WriteLine($"{next}\t{answers[next]}");
										next++;
									}
								}
							}							
						}
					}
				}
			else
			{
				for (int i = 0; i < values.Length; i++)
				{
					for (int j = 0; j < values.Length; j++)
					{
						for (int k = 0; k < values.Length; k++)
						{
							
							for (int l = 0; l < values.Length; l++)
							{
								for (int m = 0; m < values.Length; m++)
								{
									double average = (values[i] + values[j] + values[k] + values[l] + values[m]) / 5;
									double averageMath = Math.Round((values[i] + values[j] + values[k] + values[l] + values[m]) / 5);
									if (((averageMath - average) <= 0.3) && (averageMath == score))
									{
										answers[next] = ($"{values[i]} {values[j]} {values[k]} {values[l]} {values[m]}").ToString();
										//Console.WriteLine($"{next}\t{answers[next]}");
										next++;
									}
								}
							}							
						}
					}
				}
			}
			Array.Sort(answers);
			int zero = Array.LastIndexOf(answers, null);
			string[] answers2 = new string[answers.Length - (zero + 1)];
			Array.Copy(answers, zero + 1, answers2, 0, answers2.Length);
			return answers2;
		}
		static void Main(string[] args)
		{
			string[] fileScore = File.ReadAllLines("оценки.txt");
			string[] pattern = new string[] { " ", "-", "(", ")", "," };
			int[] five = fileScore[0].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
			int[] four = fileScore[1].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
			int[] three = fileScore[2].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
			int[] two = fileScore[3].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
			// vip - количество контрактников которым нельзя ставить "2"(в данном варианте программы данная функция не нужна)
			//int[] vip = fileScore[4].Split(pattern, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

			Random r = new Random();

			int random = 0;
			string[] fio = File.ReadAllLines("ФИО.txt");
			int[] temp = new int[fio.Length];
			int[] overallScore = fillingArray(five, fillingArray(four, fillingArray(three, fillingArray(two, temp /*vip[0]), vip[0]*/))));

			int[] practicalScore = new int[fio.Length];
			int[] practicalAnswer = new int[fio.Length];
			Array.Copy(overallScore, practicalScore, fio.Length);
			Array.Copy(practicalScore, practicalAnswer, fio.Length);
			int[] theoryScore = new int[fio.Length];

			for (int i = 0; i < practicalAnswer.Length; i++)
			{
				switch (practicalScore[i])
				{
					case 5:						
						random = r.Next(4, 6);
						theoryScore[i] = random;
						break;
					case 4:
						random = r.Next(4, 6);		// изменил 3 на 4 чтоб было поменьше оценок (54245 общая 4)
						theoryScore[i] = random;						
						break;
					case 3:						
						random = r.Next(3, 6);
						theoryScore[i] = random;
						break;
					case 2:						
						random = r.Next(2, 4);
						theoryScore[i] = random;
						break;
				}
			}
			
			string[] score5 = generetanigPrakticalAnswers(5);
			string[] score4 = generetanigPrakticalAnswers(4);
			string[] score3 = generetanigPrakticalAnswers(3);
			string[] score2 = generetanigPrakticalAnswers(2);
			string[] theoryAnswer = new string[fio.Length];
			for (int i = 0; i < theoryScore.Length; i++)
			{
				switch (theoryScore[i])
				{
					case 5:
						random = r.Next(0, score5.Length);
						theoryAnswer[i] = score5[random];
						break;
					case 4:
						random = r.Next(0, score4.Length);
						theoryAnswer[i] = score4[random];
						break;
					case 3:
						random = r.Next(0, score3.Length);
						theoryAnswer[i] = score3[random];
						break;
					case 2:
						random = r.Next(0, score2.Length);
						theoryAnswer[i] = score2[random];
						break;
				}
			}
			using (StreamWriter end = new StreamWriter("ведомость.txt"))
			{
				end.WriteLine("Звание\tФамилия\tТеоритическая подготовка\tОценка\tПрактическая подготовка\tОценка\tОбщая оценка");
				for (int i = 0; i < fio.Length; i++)
				{
					end.WriteLine(($"{fio[i],10}\t{theoryAnswer[i],10}\t{theoryScore[i]}\t{practicalAnswer[i]}\t{practicalScore[i]}\t{overallScore[i]}"));					
				}
			}
			Console.WriteLine("Программа завершена :-)) ");
			Thread.Sleep(1000);			
		}
	}
}
