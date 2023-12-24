using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;

namespace SocialNetwork
{
    internal class Program
    {
        public static UserService userService = new UserService();

        static void Main(string[] args)
        {
            //Console.WriteLine("Добро пожаловать в социальную сеть.");


 
                                                //Console.Write("Введите сообщение:");
                                                //break;


                    //case "2":
                    //    {
                            var userRegistrationData = new UserRegistrationData();

                            Console.WriteLine("Для создания нового профиля введите ваше имя:");
                            userRegistrationData.FirstName = Console.ReadLine();

                            Console.Write("Ваша фамилия:");
                            userRegistrationData.LastName = Console.ReadLine();

                            Console.Write("Пароль:");
                            userRegistrationData.Password = Console.ReadLine();

                            Console.Write("Почтовый адрес:");
                            userRegistrationData.Email = Console.ReadLine();

                            try
                            {
                                userService.Register(userRegistrationData);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Ваш профиль успешно создан. Теперь Вы можете войти в систему под своими учетными данными.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Введите корректное значение.");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Произошла ошибка при регистрации.");
                            }

                            break;
                        }
                }
            }

        }
    }
}
