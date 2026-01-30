using System.ComponentModel;
using System.Data.SqlClient;
using Dapper;

namespace Arbot__V_Console_
{
    public class School_Schedual
    {
        public static void Main(string[] args)
        {
            Arbot_info? info;
            Arbot_lessons timetable;
            string? pass_input;

            Console.WriteLine("Enter password:");
            Console.WriteLine("(Enter \'Forgot\' to reset password)");
            pass_input = Console.ReadLine();
            Console.WriteLine("");

            const string connectionString = "Server=localhost\\SQLEXPRESS;Database=Arbot;Trusted_Connection=True;";
            using (SqlConnection connection = new(connectionString))
            {
                connection.Open();
                string sql_command_info;
                if (pass_input.ToLower() == "forgot") {
                    Console.WriteLine("");
                    Console.WriteLine("Please enter your first name, case sensitive.");
                    string first = Console.ReadLine();
                    Console.WriteLine("Please enter your last name, case sensitive.");
                    string last = Console.ReadLine();
                    sql_command_info = "SELECT pass FROM arbot_info WHERE first_name = @First AND last_name = @Last";
                    Console.WriteLine("");
                    Console.WriteLine("Your password is:");

                    dynamic what_pass = connection.QuerySingleOrDefault(sql_command_info, new { @First = first, @Last = last });
                    int start = what_pass.IndexOf("= '") + 3;
                    int end = what_pass.IndexOf("'}");
                    int length = end - start;
                    what_pass = what_pass.substring(start, length);
                    Console.WriteLine(what_pass);

                    Console.WriteLine("");
                    Console.WriteLine("Press any key to exit:");
                    Console.ReadKey();
                    Console.WriteLine("");
                    Environment.Exit(0);
                }

                sql_command_info = "SELECT * FROM arbot_info WHERE pass = @Pass";
                info = connection.QuerySingleOrDefault<Arbot_info>(sql_command_info, new { Pass = pass_input });

                if (info == null)
                {
                    Console.WriteLine("Wrong, you moron.");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to exit:");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                string sql_command_lessons = "SELECT * FROM arbot_lessons WHERE id = @Id";
                timetable = connection.QuerySingleOrDefault<Arbot_lessons>(sql_command_lessons, new { Id = info.id });
                connection.Close();
            }

            List<string> lessons = new List<string>();
            DateTime now = DateTime.Now;
            TimeSpan current_time_obj = now.TimeOfDay;
            DateTime today_date_obj = now.Date;
            DayOfWeek today_weekday = today_date_obj.DayOfWeek;
            string period = "";
            int lesson_num = 0;
            string next_bell = "00:00:00";
            TimeSpan calculation;
            TimeSpan cal2;

            if (current_time_obj > TimeSpan.Parse("08:40:00") && current_time_obj < TimeSpan.Parse("09:10:00"))
            {
                period = "Formtime";
                lesson_num = 0;
                next_bell = "09:10:00";
            }
            else if (current_time_obj > TimeSpan.Parse("09:10:00") && current_time_obj < TimeSpan.Parse("10:00:00"))
            {
                period = "1";
                lesson_num = 1;
                next_bell = "10:00:00";
            }
            else if (current_time_obj > TimeSpan.Parse("10:00:00") && current_time_obj < TimeSpan.Parse("10:50:00"))
            {
                period = "2";
                lesson_num = 2;
                next_bell = "10:50:00";
            }
            else if (current_time_obj > TimeSpan.Parse("10:50:00") && current_time_obj < TimeSpan.Parse("11:10:00"))
            {
                period = "Breaktime";
                lesson_num = 3;
                next_bell = "11:10:00";
            }
            else if (current_time_obj > TimeSpan.Parse("11:10:00") && current_time_obj < TimeSpan.Parse("12:00:00"))
            {
                period = "3";
                lesson_num = 4;
                next_bell = "12:00:00";
            }
            else if (current_time_obj > TimeSpan.Parse("12:00:00") && current_time_obj < TimeSpan.Parse("12:50:00"))
            {
                period = "4";
                lesson_num = 5;
                next_bell = "12:50:00";
            }
            else if (current_time_obj > TimeSpan.Parse("12:50:00") && current_time_obj < TimeSpan.Parse("13:30:00"))
            {
                period = "Lunchtime";
                lesson_num = 6;
                next_bell = "13:30:00";
            }
            else if (current_time_obj > TimeSpan.Parse("13:30:00") && current_time_obj < TimeSpan.Parse("14:20:00"))
            {
                period = "5";
                lesson_num = 7;
                next_bell = "14:20:00";
            }
            else if (current_time_obj > TimeSpan.Parse("14:20:00") && current_time_obj < TimeSpan.Parse("15:10:00"))
            {
                period = "6";
                lesson_num = 8;
                next_bell = "15:10:00";
            }
            else
            {
                period = "Hometime";
                next_bell = "08:40:00";
            }

            switch (today_weekday)
            {
                case DayOfWeek.Monday:
                    lessons = new List<string> { timetable.Form, timetable.Mon_p1, timetable.Mon_p2, "Breaktime", timetable.Mon_p3, timetable.Mon_p4, timetable.Mon_lunch, timetable.Mon_p5, timetable.Mon_p6, timetable.Mon_home };
                    break;

                case DayOfWeek.Tuesday:
                    lessons = new List<string> { timetable.Form, timetable.Tue_p1, timetable.Tue_p2, "Breaktime", timetable.Tue_p3, timetable.Tue_p4, timetable.Tue_lunch, timetable.Tue_p5, timetable.Tue_p6, timetable.Tue_home };
                    break;

                case DayOfWeek.Wednesday:
                    lessons = new List<string> { timetable.Form, timetable.Wed_p1, timetable.Wed_p2, "Breaktime", timetable.Wed_p3, timetable.Wed_p4, timetable.Wed_lunch, timetable.Wed_p5, timetable.Wed_p6, timetable.Wed_home };
                    break;

                case DayOfWeek.Thursday:
                    lessons = new List<string> { timetable.Form, timetable.Thu_p1, timetable.Thu_p2, "Breaktime", timetable.Thu_p3, timetable.Thu_p4, timetable.Thu_lunch, timetable.Thu_p5, timetable.Thu_p6, timetable.Thu_home };
                    break;

                case DayOfWeek.Friday:
                    lessons = new List<string> { timetable.Form, timetable.Fri_p1, timetable.Fri_p2, "Breaktime", timetable.Fri_p3, timetable.Fri_p4, timetable.Fri_lunch, timetable.Fri_p5, timetable.Fri_p6, timetable.Fri_home };
                    break;

                case DayOfWeek.Saturday:
                    lessons = new List<string> { " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
                    break;

                case DayOfWeek.Sunday:
                    lessons = new List<string> { " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
                    break;
            }

            string current_lesson = lessons[lesson_num];
            string next_lesson = lessons[lesson_num + 1];

            calculation = TimeSpan.Parse(next_bell).Subtract(current_time_obj);
            cal2 = calculation + calculation;

            if (today_weekday != DayOfWeek.Saturday && today_weekday != DayOfWeek.Sunday)
            {
                if (cal2 >= TimeSpan.Parse("00:00:00"))
                {
                    Console.WriteLine($"The day is {today_weekday} and we are in the {period}^th period of the day and the current lesson is {current_lesson} and the next lesson is {next_lesson}. The next bell is at {next_bell} and it is in {calculation} hours, minutes and seconds respectively.");
                }
            }

            if (cal2 <= TimeSpan.Parse("00:00:00") || today_weekday == DayOfWeek.Saturday || today_weekday == DayOfWeek.Sunday)
            {
                Console.WriteLine($"The day is {today_weekday} and there are no lessons on.");
            }

            Console.WriteLine("");
            Console.WriteLine($"Your attendace is {info.attendace}%,");
            Console.WriteLine($"You have: {info.positives} positive(s),");
            Console.WriteLine($"You have: {info.negatives} negative(s),");
            Console.WriteLine($"And you have: {info.house_points} house point(s).");

            Console.WriteLine("");
            Console.WriteLine("Would you like to enter a command? - Y/N");
            string choice = Console.ReadLine();
            string sql_command;

            if (choice.ToLower() == "y")
            {
                Console.WriteLine("");
                Console.WriteLine("Enter the command:");
                Console.WriteLine("1.\t Update Positives,");
                Console.WriteLine("2.\t Update Negatives,");
                Console.WriteLine("3.\t Change lessons,");
                Console.WriteLine("4.\t Change password.");

                if (info.has_game_pass == null)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Master commands:");
                    Console.WriteLine("5 .\t Who extended their game pass, (type: \'game pass\')");
                }

                Console.WriteLine("");
                Console.WriteLine("Enter the command\'s number.");
                Console.WriteLine("");
                //}

                choice = Console.ReadLine();
                int many;
                string when;
                string what_to;
                string old_pass;
                string new_pass;
                switch (choice.ToLower())
                {
                    case "1":
                        Console.WriteLine("How many more positives have you gotten?");
                        many = Int32.Parse(Console.ReadLine());
                        using (SqlConnection connection = new(connectionString))
                        {
                            connection.Open();
                            sql_command = "UPDATE arbot_info " +
                                          "SET positives = @Many + positives " +
                                          "WHERE id = @Id;";
                            info = connection.QueryFirstOrDefault<Arbot_info>(sql_command, new { Id = info.id, @Many = many });
                            connection.Close();
                        }

                        Console.WriteLine("");
                        Console.WriteLine("Completed");
                        Console.WriteLine("");
                        break;

                    case "2":
                        Console.WriteLine("How many more negatives have you gotten?");
                        many = Int32.Parse(Console.ReadLine());
                        using (SqlConnection connection = new(connectionString))
                        {
                            connection.Open();
                            sql_command = "UPDATE arbot_info " +
                                          "SET negatives = @Many + negatives " +
                                          "WHERE id = @Id;";
                            info = connection.QuerySingleOrDefault<Arbot_info>(sql_command, new { Id = info.id, @Many = many });
                            connection.Close();
                        }

                        Console.WriteLine("");
                        Console.WriteLine("Completed.");
                        Console.WriteLine("");
                        break;

                    case "3":
                        Console.WriteLine("What period class are you changing? It can not be form room.");
                        Console.WriteLine("use the format: ");
                        Console.WriteLine("\t first_three_letters_of_the_day_(start_with_capital) _ if_it\'s_in_a_period_use_p_then_period_number");
                        Console.WriteLine("\t e.g. Mon_p2  -  this means period 2 on Monday");
                        Console.WriteLine("\t e.g. Thu_lunch  -  this means lunchtime on Thursday (Note: lunch not lunchtime, same with hometime, home not hometime)");
                        when = Console.ReadLine();

                        Console.WriteLine("What class are you changing it to?");
                        what_to = Console.ReadLine();

                        using (SqlConnection connection = new(connectionString))
                        {
                            connection.Open();
                            sql_command = "UPDATE arbot_lessons" +
                                          "SET @When = @What" +
                                          "WHERE id = @Id;";
                            timetable = connection.QuerySingleOrDefault<Arbot_lessons>(sql_command, new { Id = info.id, @When = when, @What = what_to });
                            connection.Close();
                        }

                        Console.WriteLine("");
                        Console.WriteLine("Completed.");
                        Console.WriteLine("");
                        break;

                    case "4":
                        Console.WriteLine("Enter old password:");
                        old_pass = Console.ReadLine();
                        using (SqlConnection connection = new(connectionString))
                        {
                            connection.Open();
                            sql_command = "SELECT pass FROM Arbot_info";
                            string what_pass = connection.Query<Arbot_info>(sql_command).ToString();

                            if (old_pass == what_pass)
                            {
                                Console.WriteLine("Enter new password:");
                                new_pass = Console.ReadLine();
                                sql_command = "Update Arbot_info " +
                                              "SET pass = @New_Pass " +
                                              "Where id = @Id;";
                                info = connection.QuerySingleOrDefault<Arbot_info>(sql_command, new { New_Pass = new_pass, @Id = info.id });
                            }
                            else
                            {
                                Console.WriteLine("Wrong.");
                            }
                            connection.Close();

                            Console.WriteLine("");
                            Console.WriteLine("Complete");
                            Console.WriteLine("");
                        }
                        break;

                    case "6":
                        Console.WriteLine("Enter last name:");
                        when = Console.ReadLine();
                        Console.WriteLine("How many more weeks have they paid for?");
                        many = Int32.Parse(Console.ReadLine());
                        using (SqlConnection connection = new(connectionString))
                        {
                            connection.Open();
                            sql_command = "Update arbot_info " +
                                          "Set has_game_pass = DateAdd(weeks, @Many, has_game_pass) " +
                                          "Where last_name = @Who";
                            connection.QueryFirstOrDefault<Arbot_info>(sql_command, new { @Who = when, @Many = many });
                            connection.Close();
                        }
                        Console.WriteLine("");
                        Console.WriteLine("Complete");
                        Console.WriteLine("");
                        break;
                }
            }
            else
            {
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    public class Arbot_lessons
        {
            public string? Id { get; set; }
            public string? Form { get; set; }
            public string? Mon_p1 { get; set; }
            public string? Mon_p2 { get; set; }
            public string? Mon_p3 { get; set; }
            public string? Mon_p4 { get; set; }
            public string? Mon_lunch { get; set; }
            public string? Mon_p5 { get; set; }
            public string? Mon_p6 { get; set; }
            public string? Mon_home { get; set; }

            public string? Tue_p1 { get; set; }
            public string? Tue_p2 { get; set; }
            public string? Tue_p3 { get; set; }
            public string? Tue_p4 { get; set; }
            public string? Tue_lunch { get; set; }
            public string? Tue_p5 { get; set; }
            public string? Tue_p6 { get; set; }
            public string? Tue_home { get; set; }

            public string? Wed_p1 { get; set; }
            public string? Wed_p2 { get; set; }
            public string? Wed_p3 { get; set; }
            public string? Wed_p4 { get; set; }
            public string? Wed_lunch { get; set; }
            public string? Wed_p5 { get; set; }
            public string? Wed_p6 { get; set; }
            public string? Wed_home { get; set; }

            public string? Thu_p1 { get; set; }
            public string? Thu_p2 { get; set; }
            public string? Thu_p3 { get; set; }
            public string? Thu_p4 { get; set; }
            public string? Thu_lunch { get; set; }
            public string? Thu_p5 { get; set; }
            public string? Thu_p6 { get; set; }
            public string? Thu_home { get; set; }

            public string? Fri_p1 { get; set; }
            public string? Fri_p2 { get; set; }
            public string? Fri_p3 { get; set; }
            public string? Fri_p4 { get; set; }
            public string? Fri_lunch { get; set; }
            public string? Fri_p5 { get; set; }
            public string? Fri_p6 { get; set; }
            public string? Fri_home { get; set; }
        }

        public class Arbot_info
        {
            public string? id { get; set; }
            public string? pass { get; set; }
            public string? first_name { get; set; }
            public string? last_name { get; set; }
            public int? positives { get; set; }
            public int? negatives { get; set; }
            public int? house_points { get; set; }
            public int? attendace { get; set; }
            public DateOnly? has_game_pass { get; set; }
        }
    }
}