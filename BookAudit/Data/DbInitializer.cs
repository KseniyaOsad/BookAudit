using BookAudit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAudit.Data
{
    public class DbInitializer
    {
        public static void Initialize(BookContext context)
        {
            context.Database.EnsureCreated();

            // Смотрим наличие книг.
            if (context.Book.Any())
            {
                return;   
                // БД заполнена => ничего не делаем.
            }

            // Создание авторов книг.
            Author Troelsen = new Author() { Name = "Эндрю Троелсен" };
            context.Author.Add(Troelsen);
            Author Rihter = new Author() { Name = "Джеффри Рихтер" };
            context.Author.Add(Rihter);
            Author Freeman = new Author() { Name = "Адам Фримен" };
            context.Author.Add(Freeman);
            context.SaveChanges();
            // Сохранение авторов книг.


            // Создание книг.
            var books = new Book[]
            {
                new Book() { AuthorId=Troelsen.Id, Name = "Язык программирования C# 7 и платформы .NET и .NET Core", Description="Эта классическая книга представляет собой всеобъемлющий источник сведений о языке программирования C# и о связанной с ним инфраструктуре. В 8-м издании книги вы найдете описание функциональных возможностей самых последних версий C# 7.0 и 7.1 и .NET 4.7, а также совершенно новые главы, посвященные легковесной межплатформенной инфраструктуре .NET Core. Перепроектированные инфраструктуры ASP.NET Core 2.0 и Entity Framework (EF) Core 2.0 рассматриваются наряду с последними обновлениями, внесенными в .NET 4.7, которые затронули Windows Presentation Foundation (WPF), Windows Communication Foundation (WCF), ASP.NET MVC и ASP.NET Web API.Погрузитесь в книгу и выясните, почему на протяжении более 15 лет она была лидером у разработчиков по всему миру."},
                new Book() { AuthorId=Troelsen.Id, Name = "Pro C# 7: с .NET и .NET Core", Description="Это важное классическое название обеспечивает исчерпывающую основу для языка программирования C # и фреймворков, в которых он живет. Теперь, в его 8-м издании, вы найдете здесь все самые последние функции C # 7.1 и .NET 4.7, а также четыре новые главы, посвященные Облегченная кроссплатформенная платформа Microsoft, .NET Core, до .NET Core 2.0 включительно. Охват ASP.NET Core, Entity Framework (EF) Core и других компонентов находится рядом с последними обновлениями .NET, включая Windows Presentation Foundation (WPF), Windows Communication Foundation (WCF) и ASP.NET MVC."},
                new Book() { AuthorId=Rihter.Id, Name = "CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#", Description="Книга 'CLR via C#. Программирование на платформе Microsoft .NET Framework 4.5 на языке C#', выходящая в четвертом издании и уже ставшая классическим учебником по программированию, подробно описывает внутреннее устройство и функционирование общеязыковой исполняющей среды (CLR) Microsoft .NET Framework версии 4.5. Написанная признанным экспертом в области программирования Джеффри Рихтером, много лет являющимся консультантом команды разработчиков .NET Framework компании Microsoft, книга научит вас создавать по-настоящему надежные приложения любого вида, в том числе с использованием Microsoft Silverlight, ASP.NET, Windows Presentation Foundation и т.д. Четвертое издание полностью обновлено в соответствии со спецификацией платформы .NET Framework 4.5, а также среды Visual Studio 2012 и C# 5.0."},
                new Book() { AuthorId=Freeman.Id, Name = "ASP.NET Core MVC 2 с примерами на C# для профессионалов", Description="В этом 7-м издании книга-бестселлер по MVC обновлена с учетом версии ASP.NET Core MVC 2. Она содержит подробные объяснения функциональности Core MVC, которая позволяет разработчикам выпускать более экономные, оптимизированные под облако и готовые к функционированию на мобильных устройствах приложения для платформы .NET." },
                new Book() { AuthorId=Freeman.Id, Name = "Pro ASP.NET Core 3 (разработка облачных веб-приложений с использованием MVC 3, Blazor и Razor Pages)", Description="Профессиональные разработчики готовы создавать более компактные приложения для платформы ASP.NET Core. В этом выпуске ASP.NET Core 3 рассматривается в контексте и подробно рассматриваются инструменты и методы, необходимые для создания современных расширяемых веб-приложений. Рассмотрены новые функции и возможности, такие как MVC 3, Razor Pages, Blazor Server и Blazor WebAssembly, а также показано, как их можно применить на практике."},
                new Book() { AuthorId=Freeman.Id, Name = "Angular для профессионалов", Description="Выжмите из Angular — ведущего фреймворка для динамических приложений JavaScript — всё. Адам Фримен начинает с описания MVC и его преимуществ, затем показывает, как эффективно использовать Angular, охватывая все этапы: начиная с основ и до самых передовых возможностей, которые кроются в глубинах этого фреймворка."} 
            };
            foreach (Book book in books)
            {
                context.Book.Add(book);
            }
            context.SaveChanges();
            // Сохранение книг.
        }
    }
}
