-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 26 Lut 2024, 21:32
-- Wersja serwera: 10.4.25-MariaDB
-- Wersja PHP: 7.4.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `projektprogramowanie`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `produkt`
--

CREATE TABLE `produkt` (
  `id` int(11) NOT NULL,
  `nazwa_produktu` varchar(255) DEFAULT NULL,
  `data_rozpoczecia` date DEFAULT NULL,
  `data_zakonczenia` date DEFAULT NULL,
  `wielkosc` varchar(50) DEFAULT NULL,
  `ilosc` int(11) DEFAULT NULL,
  `uzytkownik_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `produkt`
--

INSERT INTO `produkt` (`id`, `nazwa_produktu`, `data_rozpoczecia`, `data_zakonczenia`, `wielkosc`, `ilosc`, `uzytkownik_id`) VALUES
(135, 'Produkt 1', '2023-01-01', '2023-01-10', 'male', 15, 1),
(136, 'Produkt 2', '2023-02-05', '2023-02-20', 'srednie', 10, 2),
(137, 'Produkt 3', '2023-03-10', '2023-03-25', 'duze', 5, 3),
(138, 'Produkt 4', '2023-04-15', '2023-05-01', 'male', 20, 4),
(139, 'Produkt 5', '2023-05-20', '2023-06-05', 'srednie', 25, 5),
(140, 'Produkt 6', '2023-06-25', '2023-07-10', 'duze', 30, 6),
(141, 'Produkt 7', '2023-07-30', '2023-08-10', 'male', 1, 7),
(142, 'Produkt 8', '2023-08-05', '2023-08-15', 'srednie', 2, 8),
(143, 'Produkt 9', '2023-09-10', '2023-09-25', 'duze', 3, 9),
(144, 'Produkt 10', '2023-10-15', '2023-10-30', 'male', 4, 10),
(145, 'Produkt 11', '2023-11-20', '2023-12-05', 'srednie', 5, 1),
(146, 'Produkt 12', '2024-01-01', '2024-01-15', 'duze', 6, 2),
(147, 'Produkt 13', '2024-02-05', '2024-02-20', 'male', 7, 3),
(148, 'Produkt 14', '2023-03-10', '2024-03-25', 'srednie', 8, 4),
(149, 'Produkt 15', '2023-04-15', '2024-05-01', 'duze', 9, 5),
(151, 'Produkt 17', '2023-06-25', '2024-07-10', 'srednie', 11, 7),
(152, 'Produkt 18', '2023-07-30', '2024-08-10', 'duze', 12, 8),
(153, 'Produkt 19', '2023-08-15', '2024-08-30', 'male', 13, 9),
(154, 'Produkt 20', '2023-09-20', '2024-10-05', 'srednie', 14, 10),
(155, 'Produkt 21', '2023-10-10', '2024-10-25', 'duze', 15, 1),
(156, 'Produkt 22', '2023-11-15', '2024-11-30', 'male', 16, 2),
(157, 'Produkt 23', '2023-12-20', '2025-01-05', 'srednie', 17, 3),
(158, 'Produkt 24', '2023-01-10', '2025-01-25', 'duze', 18, 4),
(159, 'Produkt 25', '2023-02-20', '2025-03-07', 'male', 19, 5),
(160, 'Produkt 26', '2023-03-15', '2025-03-30', 'srednie', 20, 6),
(161, 'Produkt 27', '2023-04-20', '2025-05-05', 'duze', 21, 7),
(162, 'Produkt 28', '2023-05-10', '2025-05-25', 'male', 22, 8),
(163, 'Produkt 29', '2023-06-15', '2025-06-30', 'srednie', 23, 9),
(164, 'Produkt 30', '2023-07-20', '2025-08-04', 'duze', 24, 10),
(165, 'honda civic', '2024-02-21', '2024-03-21', 'duze', 1, 15),
(166, 'czołg', '2023-02-01', '2024-01-14', 'srednie', 15, 15);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `role` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `role`) VALUES
(1, 'admin', 'admin123', 'admin'),
(2, 'user1', 'password1', 'pracownik'),
(3, 'user2', 'password2', 'pracownik'),
(4, 'user3', 'password3', 'pracownik');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `uzytkownicy`
--

CREATE TABLE `uzytkownicy` (
  `id` int(11) NOT NULL,
  `imie` varchar(255) DEFAULT NULL,
  `nazwisko` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `uzytkownicy`
--

INSERT INTO `uzytkownicy` (`id`, `imie`, `nazwisko`) VALUES
(1, 'Jan', 'Kowalski'),
(2, 'Anna', 'Nowak'),
(3, 'Marek', 'Duda'),
(4, 'Ewa', 'Wiśniewska'),
(5, 'Piotr', 'Lewandowski'),
(6, 'Karolina', 'Wójcik'),
(7, 'Michał', 'Kamiński'),
(8, 'Magdalena', 'Kowalczyk'),
(9, 'Kamil', 'Zieliński'),
(10, 'adam ', 'malysz'),
(15, 'Katarzyna', 'Walczak');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `produkt`
--
ALTER TABLE `produkt`
  ADD PRIMARY KEY (`id`),
  ADD KEY `uzytkownik_id` (`uzytkownik_id`);

--
-- Indeksy dla tabeli `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indeksy dla tabeli `uzytkownicy`
--
ALTER TABLE `uzytkownicy`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `produkt`
--
ALTER TABLE `produkt`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=167;

--
-- AUTO_INCREMENT dla tabeli `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT dla tabeli `uzytkownicy`
--
ALTER TABLE `uzytkownicy`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `produkt`
--
ALTER TABLE `produkt`
  ADD CONSTRAINT `produkt_ibfk_1` FOREIGN KEY (`uzytkownik_id`) REFERENCES `uzytkownicy` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
