using System;
using MySql.Data.MySqlClient;
using ConsoleApp1.DatabaseManager;
using ConsoleApp1.Entity;
using ConsoleApp1.User;
using ConsoleApp1.Produkt;
using ConsoleApp1.UserLogin;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Podaj login:");
        string username = Console.ReadLine();
        Console.WriteLine("Podaj hasło:");
        string password = Console.ReadLine();

        DatabaseManager dbManager = new DatabaseManager();
        if (dbManager.ValidateLogin(username, password))
        {
            Console.WriteLine("Zalogowano pomyślnie.");
            if(username == "admin")
            {
                AdminMenu();
                
            }
            else
            {
                WorkerMenu();
               
            }
            
        }
        else
        {
            Console.WriteLine("Nieprawidłowy login lub hasło.");
        }
        
    }
   
    static void WorkerMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Wyświetl użytkowników");
            Console.WriteLine("2. Dodaj użytkownika");
            Console.WriteLine("3. Usuń użytkownika");
            Console.WriteLine("4. Wyświetl produkty");
            Console.WriteLine("5. Dodaj produkt");
            Console.WriteLine("6. Usuń produkt");
           Console.WriteLine("7. Wyjście");
            Console.Write("Wybierz opcję: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayUsers();
                    break;
                case "2":
                    AddUser();
                    break;
                case "3":
                    RemoveUser();
                    break;
                case "4":
                    DisplayProducts();
                    break;
                case "5":
                    AddProduct();
                    break;
                case "6":
                    RemoveProduct();
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
    static void AdminMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Wyświetl użytkowników");
            Console.WriteLine("2. Dodaj użytkownika");
            Console.WriteLine("3. Usuń użytkownika");
            Console.WriteLine("4. Wyświetl produkty");
            Console.WriteLine("5. Dodaj produkt");
            Console.WriteLine("6. Usuń produkt");
            Console.WriteLine("7. Statystyki");
            Console.WriteLine("8. Wyjście");
            Console.Write("Wybierz opcję: ");

            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DisplayUsers();
                    break;
                case "2":
                    AddUser();
                    break;
                case "3":
                    RemoveUser();
                    break;
                case "4":
                    DisplayProducts();
                    break;
                case "5":
                    AddProduct();
                    break;
                case "6":
                    RemoveProduct();
                    break;
                case "7":
                    GenerateStatistics();
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
    static void DisplayUsers()
    {
        Console.WriteLine("Dane z tabeli użytkownicy:");
        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM uzytkownicy;";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        string imie = reader.GetString("imie");
                        string nazwisko = reader.GetString("nazwisko");
                        Console.WriteLine($"ID: {id}, Imię: {imie}, Nazwisko: {nazwisko}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd połączenia z bazą danych: " + ex.Message);
            }
        }
    }

    static void AddUser()
    {
        Console.Write("Podaj imię nowego użytkownika: ");
        string imie = Console.ReadLine();

        Console.Write("Podaj nazwisko nowego użytkownika: ");
        string nazwisko = Console.ReadLine();

        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";
        string query = "INSERT INTO uzytkownicy (imie, nazwisko) VALUES (@imie, @nazwisko)";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@imie", imie);
                command.Parameters.AddWithValue("@nazwisko", nazwisko);
                command.ExecuteNonQuery();
                Console.WriteLine("Użytkownik dodany pomyślnie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd dodawania użytkownika: " + ex.Message);
            }
        }
    }

    static void RemoveUser()
    {
        Console.Write("Podaj ID użytkownika do usunięcia: ");
        int userId;
        if (!int.TryParse(Console.ReadLine(), out userId))
        {
            Console.WriteLine("Nieprawidłowy format ID użytkownika.");
            return;
        }

        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";
        string checkProductQuery = "SELECT COUNT(*) FROM produkt WHERE uzytkownik_id = @id";
        string deleteProductsQuery = "DELETE FROM produkt WHERE uzytkownik_id = @id";
        string deleteUserQuery = "DELETE FROM uzytkownicy WHERE id = @id";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand checkProductCommand = new MySqlCommand(checkProductQuery, connection);
                checkProductCommand.Parameters.AddWithValue("@id", userId);
                int productCount = Convert.ToInt32(checkProductCommand.ExecuteScalar());

                if (productCount > 0)
                {
                    MySqlCommand deleteProductsCommand = new MySqlCommand(deleteProductsQuery, connection);
                    deleteProductsCommand.Parameters.AddWithValue("@id", userId);
                    deleteProductsCommand.ExecuteNonQuery();
                    Console.WriteLine($"Usunięto {productCount} produktów powiązanych z użytkownikiem.");
                }

                MySqlCommand deleteUserCommand = new MySqlCommand(deleteUserQuery, connection);
                deleteUserCommand.Parameters.AddWithValue("@id", userId);
                int rowsAffected = deleteUserCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Użytkownik usunięty pomyślnie.");
                else
                    Console.WriteLine("Nie znaleziono użytkownika o podanym ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd usuwania użytkownika: " + ex.Message);
            }
        }
    }

    static void DisplayProducts()
    {
        Console.WriteLine("Dane z tabeli produkt:");
        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "SELECT produkt.id, produkt.nazwa_produktu, produkt.wielkosc, produkt.ilosc, produkt.data_rozpoczecia, produkt.data_zakonczenia, uzytkownicy.imie, uzytkownicy.nazwisko FROM produkt JOIN uzytkownicy ON produkt.uzytkownik_id = uzytkownicy.id order by produkt.id asc";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_produkt = reader.GetInt32("id");
                        string Nazwa_produktu = reader.GetString("nazwa_produktu");
                        string wielkosc = reader.GetString("wielkosc");
                        int ilosc = reader.GetInt32("ilosc");
                        DateTime data_rozpoczecia = reader.GetDateTime("data_rozpoczecia");
                        DateTime data_zakonczenia = reader.GetDateTime("data_zakonczenia");
                        string imie = reader.GetString("imie");
                        string nazwisko = reader.GetString("nazwisko");



                        Produkt produkt = new Produkt
                        {
                            Ilość = ilosc,
                            Wielkość = wielkosc,
                            DataRozpoczęcia = data_rozpoczecia,
                            DataZakończenia = data_zakonczenia
                        };

                        TimeSpan roznica = data_zakonczenia - data_rozpoczecia;
                        int liczbaDni = (int)roznica.TotalDays;
                        decimal koszt = 0;
                        decimal kosztodsetek = 0;
                        
                        int dniPoTerminie = (DateTime.Today - data_zakonczenia).Days;
                        if (dniPoTerminie <= 0)
                        {
                            if (wielkosc == "male")
                            {
                                koszt = liczbaDni * ilosc * 1;
                            }
                            else if (wielkosc == "srednie")
                            {
                                koszt = liczbaDni * ilosc * 3;
                            }
                            else if (wielkosc == "duze")
                            {
                                koszt = liczbaDni * ilosc * 5;
                            }
                            Console.WriteLine("----------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"ID: {id_produkt}, Nazwa Produktu: {Nazwa_produktu}, Wielkość: {wielkosc}, Ilość: {ilosc}, Data rozpoczęcia: {data_rozpoczecia}, Data zakończenia: {data_zakonczenia}, Użytkownik: {imie} {nazwisko},Koszt:{koszt} zł");
                        }
                        else if(dniPoTerminie > 0)
                        {
                            if (wielkosc == "male")
                            {
                                koszt = (liczbaDni * ilosc * 1) + (dniPoTerminie * ilosc * 2);
                                kosztodsetek = dniPoTerminie * ilosc * 2;
                            }
                            else if (wielkosc == "srednie")
                            {
                                koszt = (liczbaDni * ilosc * 3) + (dniPoTerminie * ilosc * 6);
                                kosztodsetek = dniPoTerminie * ilosc * 6;
                            }
                            else if (wielkosc == "duze")
                            {
                                koszt = (liczbaDni * ilosc * 5) + (dniPoTerminie * ilosc * 10);
                                kosztodsetek = dniPoTerminie * ilosc * 10;
                            }
                            Console.WriteLine("----------------------------------------------------------------------------------------------------");
                            Console.WriteLine($"ID: {id_produkt}, Nazwa Produktu: {Nazwa_produktu}, Wielkość: {wielkosc}, Ilość: {ilosc}, Data rozpoczęcia: {data_rozpoczecia}, Data zakończenia: {data_zakonczenia}, Użytkownik: {imie} {nazwisko},Koszt do teraz:{koszt} zł,Koszt odsetek:{kosztodsetek}");
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd połączenia z bazą danych: " + ex.Message);
            }
        }
    }
    static void GenerateStatistics()
    {
        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";

        // Liczenie liczby produktów w bazie danych
        int totalProducts = 0;
        string countProductsQuery = "SELECT COUNT(*) FROM produkt";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(countProductsQuery, connection);
                totalProducts = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas pobierania liczby produktów: " + ex.Message);
            }
        }

        // Obliczanie średniej ilości produktów
        double averageQuantity = 0;
        string averageQuantityQuery = "SELECT AVG(ilosc) FROM produkt";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(averageQuantityQuery, connection);
                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    averageQuantity = Convert.ToDouble(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas obliczania średniej ilości produktów: " + ex.Message);
            }
        }
        double full_magazine_value = 0;
        string value_query = "SELECT SUM(DATEDIFF(data_zakonczenia, data_rozpoczecia) * ilosc * CASE WHEN wielkosc = 'male' THEN 2 WHEN wielkosc = 'srednie' THEN 3 WHEN wielkosc = 'duze' THEN 6 ELSE 1 END) AS wynik FROM produkt;";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(value_query, connection);
                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    full_magazine_value = Convert.ToDouble(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas obliczania średniej ilości produktów: " + ex.Message);
            }
        }

        // Wyświetlanie uzyskanych statystyk
        Console.WriteLine($"Liczba produktów: {totalProducts}");
        Console.WriteLine($"Średnia ilość produktów: {averageQuantity}");
        Console.WriteLine($"Suma wartosci magazynu: {full_magazine_value} zł");
        // Możesz dodać więcej statystyk, w zależności od tego, jakie informacje chcesz uzyskać.
    }


    static void AddProduct()
    {
        Console.Write("Podaj nazwę nowego produktu: ");
        string nazwa = Console.ReadLine();

        Console.Write("Podaj datę rozpoczęcia (RRRR-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataRozpoczęcia))
        {
            Console.WriteLine("Nieprawidłowy format daty.");
            return;
        }

        Console.Write("Podaj datę zakończenia (RRRR-MM-DD): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataZakończenia))
        {
            Console.WriteLine("Nieprawidłowy format daty.");
            return;
        }

        if (dataZakończenia <= dataRozpoczęcia)
        {
            Console.WriteLine("Data zakończenia musi być późniejsza niż data rozpoczęcia.");
            return;
        }

        Console.Write("Podaj ilość: ");
        if (!int.TryParse(Console.ReadLine(), out int ilość) || ilość <= 0)
        {
            Console.WriteLine("Nieprawidłowy format ilości.");
            return;
        }

        Console.WriteLine("Wybierz wielkość:");
        Console.WriteLine("1. Małe");
        Console.WriteLine("2. Średnie");
        Console.WriteLine("3. Duże");
        Console.Write("Wybierz opcję: ");
        string sizeOption = Console.ReadLine();
        string rozmiar = "";
        switch (sizeOption)
        {
            case "1":
                rozmiar = "male";
                break;
            case "2":
                rozmiar = "srednie";
                break;
            case "3":
                rozmiar = "duze";
                break;
            default:
                Console.WriteLine("Nieprawidłowa opcja wielkości.");
                return;
        }

        Console.Write("Podaj ID użytkownika: ");
        if (!int.TryParse(Console.ReadLine(), out int użytkownikId) || użytkownikId <= 0)
        {
            Console.WriteLine("Nieprawidłowy format ID użytkownika.");
            return;
        }

        // Połączenie z bazą danych
        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";
        string query = "INSERT INTO produkt (nazwa_produktu, wielkosc, ilosc, data_rozpoczecia, data_zakonczenia, uzytkownik_id) VALUES (@nazwa, @wielkosc, @ilosc, @dataRozpoczęcia, @dataZakończenia, @użytkownikId)";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nazwa", nazwa);
                command.Parameters.AddWithValue("@wielkosc", rozmiar);
                command.Parameters.AddWithValue("@ilosc", ilość);
                command.Parameters.AddWithValue("@dataRozpoczęcia", dataRozpoczęcia);
                command.Parameters.AddWithValue("@dataZakończenia", dataZakończenia);
                command.Parameters.AddWithValue("@użytkownikId", użytkownikId);
                command.ExecuteNonQuery();
                Console.WriteLine("Produkt dodany pomyślnie.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd dodawania produktu: " + ex.Message);
            }
        }
    }

    static void RemoveProduct()
    {
        Console.Write("Podaj ID produktu do usunięcia: ");
        // metoda sprawdza czy uzytownik podal cyfre
        if (!int.TryParse(Console.ReadLine(), out int productId) || productId <= 0)
        {
            Console.WriteLine("Nieprawidłowy format ID produktu.");
            return;
        }

        string connectionString = "Server=localhost;Port=3306;Database=projektprogramowanie;Uid=root;";
        string deleteProductQuery = "DELETE FROM produkt WHERE id = @id";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand deleteProductCommand = new MySqlCommand(deleteProductQuery, connection);
                deleteProductCommand.Parameters.AddWithValue("@id", productId);
                int rowsAffected = deleteProductCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                    Console.WriteLine("Produkt usunięty pomyślnie.");
                else
                    Console.WriteLine("Nie znaleziono produktu o podanym ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd usuwania produktu: " + ex.Message);
            }
        }
    }
}










