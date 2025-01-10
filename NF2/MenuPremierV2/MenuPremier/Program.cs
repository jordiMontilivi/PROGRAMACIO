namespace MenuPremier
{
    internal class Program
    {

        static void Main(string[] args)
        {
            const string FILE_TEAMS = @"..\..\..\..\FITXERS\TEAMS.TXT";
            const string FILE_MATCHES = @"..\..\..\..\FITXERS\MATCHES.TXT";
            ConsoleKeyInfo tecla;
            do
            {
                Console.Clear();
                MostrarMenu();
                tecla = Console.ReadKey();
                switch (tecla.Key)
                {
                    case ConsoleKey.D1:
                        DoSearchTeam(FILE_TEAMS);
                        break;
                    case ConsoleKey.D2:
                        DoGetGoalsTeam(FILE_TEAMS, FILE_MATCHES);
                        break;
                    case ConsoleKey.D3:
                        DoGetMatch(FILE_TEAMS, FILE_MATCHES);
                        break;
                    case ConsoleKey.D4:
                        DoGetPointsTeam(FILE_TEAMS, FILE_MATCHES);
                        break;
                    case ConsoleKey.D0:
                        MsgNextScreen("PRESS ANY KEY 2 EXIT");

                        break;
                    default:
                        MsgNextScreen("Error. Prem una tecla per tornar al menú...");
                        break;
                }

            } while (tecla.Key != ConsoleKey.D0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void MsgNextScreen(string msg)
        {
            Console.WriteLine(msg);
            Console.ReadKey();
        }
        /// <summary>
        /// Es demana per teclat l'abreviatura d'un equip i s'informa si l'equip existeix o no. 
        /// En cas que existeixi, es mostren les dades de l'equip
        /// </summary>
        /// <param name="fileTeams">fitxer que conté tots els equips</param>
        public static void DoSearchTeam(string fileTeams)
        {
            string abv; string nomEquip;
            Console.Clear();
            Console.Write("ABREVIATURA DE L'EQUIP -->"); abv = Console.ReadLine();
            nomEquip = GetTeam(fileTeams, abv);
            if (nomEquip != null)
                Console.WriteLine($"{abv} CORRESPON A L'EQUIP AMB NOM {nomEquip}");
            else
                Console.WriteLine($"NO EXISTEIX CAP EQUIP AMB ABREVIATURA = {abv}");

            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }
        /// <summary>
        /// Es retorna el nom de l'equip a partir de la seva abreviatura
        /// </summary>
        /// <param name="fileTeams">fitxer que conté els equips</param>
        /// <param name="abreviatura">abreviatura de l'equip a cercar</param>
        /// <returns>el nom de l'equip trobat en el fitxer fileTeams que tingui com a abreviatura el valor del paràmetre 'abreviatura'
        /// si l'equip no existeix, retornem null</returns>
        public static string GetTeam(string fileTeams, string abreviatura)
        {
            string nomEquip = null;
            string abreviaturaDelFitxer;
            bool trobat = false;
            StreamReader sRTeams = new StreamReader(fileTeams);
            string linia = sRTeams.ReadLine();

            while (linia != null && !trobat)
            {
                abreviaturaDelFitxer = sRTeams.ReadLine();
                if (abreviaturaDelFitxer == abreviatura) { trobat = true; nomEquip = linia; }
                else linia = sRTeams.ReadLine();
            }
            sRTeams.Close();
            return nomEquip;
        }
        /// <summary>
        /// demana una abreviatura d'equip per teclat. 
        /// Si l'equip existeix, mostra el nom i els gols totals fet per l'equip en tots els seus partits.
        /// Si no existeix, es dona un msg d'error i tornem al menú principal
        /// </summary>
        /// <param name="fileTeams"></param>
        /// <param name="fileMatches"></param>
        public static void DoGetGoalsTeam(string fileTeams, string fileMatches)
        {
            string abv, nomEquip; int nGols;
            Console.Clear();
            Console.Write("ABREVIATURA DE L'EQUIP-->");
            abv = Console.ReadLine();
            nomEquip = GetTeam(fileTeams, abv);
            if (nomEquip == null) Console.WriteLine("NO EXISTEIX CAP EQUIP AMB AQUESTA ABREVIATURA");
            else
            {
                nGols = GetGoalsTeam(fileMatches, abv);
                Console.WriteLine($"{nomEquip} HA FET {nGols} DURANT LA TEMPORADA");
            }
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }
        /// <summary>
        /// Retorna el total de gols  fets durant tota la temporada per l'equip amb l'abreviatura paràmetre
        /// </summary>
        /// <param name="fileMatches">fitxer que conté tots els partits</param>
        /// <param name="abreviatura">abreviatura de l'equip. L'abreviatura ha d'existir</param>
        /// <returns>el total de gols fets per l'equip amb l'abreviatura donada pel paràmetre 'abreviatura'</returns>
        public static int GetGoalsTeam(string fileMatches, string abreviatura)
        {
            int nGols = 0; int goals; string abr;
            StreamReader srTeams = new StreamReader(fileMatches);
            string linia = srTeams.ReadLine();
            while (linia != null)
            {
                abr = srTeams.ReadLine();
                if (abr == abreviatura)
                {
                    goals = Convert.ToInt32(srTeams.ReadLine());
                    srTeams.ReadLine(); srTeams.ReadLine();   //PASSEM LES DADES DE L'EQUIP VISITANT
                    nGols += goals;
                }
                else
                {
                    srTeams.ReadLine();  //PASSEM ELS GOLS LOCALS
                    abr = srTeams.ReadLine();
                    if (abr == abreviatura)
                    {
                        goals = Convert.ToInt32(srTeams.ReadLine());
                        nGols += goals;
                    }
                    else
                    { srTeams.ReadLine(); }
                }
                linia = srTeams.ReadLine();  //AGAFEM LA SEGÜENT DATA

            }
            srTeams.Close();
            return nGols;
        }


        /// <summary>
        /// Demana per teclat una data vàlida, una abreviatura vàlida de l'equip local,
        /// una abreviatura vàlida de l'equip visitant i cerca en el fitxer fileMatches
        /// el resultat del partit disputat pels dos equips en la data donada.
        /// Si la data és no vàlida, o alguna abreviatura és no vàlida o no es troba un
        /// partit dels dos equips en la data donada, es donarà el missatge d'error corresponent
        /// i es tornarà al menú principal
        /// </summary>
        /// <param name="fileTeams"></param>
        /// <param name="fileMatches"></param>
        public static void DoGetMatch(string fileTeams, string fileMatches)
        {
            int dataPartit, dia, mes, any; int tmp;
            string dataStr; string abvHome, nameHome, abvAway, nameAway;
            string infoMatch;
            Console.Clear();
            Console.Write("ENTRA LA DATA DEL PARTIT EN FORMAT DDMMAAAA. Ha de ser una data de l'any 2022 o 2023-->");
            dataPartit = Convert.ToInt32(Console.ReadLine());
            any = dataPartit % 10000;
            tmp = dataPartit / 10000;
            mes = tmp % 100; dia = tmp / 100;
            dataStr = $"{dia:00}/{mes:00}/{any:0000}";
            if (!ValidDate(dia, mes, any))
                Console.WriteLine($"{dataStr} NO ESTÀ ENTRE 1/1/2022 i 31/12/2023 O ÉS NO VÀLIDA");
            else
            {
                Console.Write("ABREVIATURA DE L'EQUIP LOCAL-->");
                abvHome = Console.ReadLine();
                nameHome = GetTeam(fileTeams, abvHome);
                if (nameHome == null) Console.WriteLine("NO EXISTEIX CAP EQUIP AMB AQUESTA ABREVIATURA");
                else
                {
                    Console.Write("ABREVIATURA DE L'EQUIP VISITANT-->");
                    abvAway = Console.ReadLine();
                    nameAway = GetTeam(fileTeams, abvAway);
                    if (nameAway == null) Console.WriteLine("NO EXISTEIX CAP EQUIP AMB AQUESTA ABREVIATURA");
                    else
                    {
                        infoMatch = GetMatch(fileMatches, abvHome, nameHome, abvAway, nameAway, dataStr);
                        if (infoMatch == null) Console.WriteLine($"NO EXISTEIX CAP PARTIT DE {nameHome} CONTRA {nameAway} en la data {dataStr}");
                        else
                        {
                            Console.WriteLine($"RESULTAT DEL PARTIT JUGAT EL {dataStr}:");
                            Console.WriteLine(infoMatch);
                        }
                    }
                }
            }
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }

        /// <summary>
        /// retorna un string en format DATA:09/04/2023 PARTIT : LIVERPOOL 2 - 2 ARSENAL
        /// </summary>
        /// <param name="fileMatches">fitxer que conté tots els partits</param>
        /// <param name="homeTeamABV">abreviatura de l'equip local. Ha d'existir</param>
        /// <param name="homeTeamName">nom de l'equip local.</param>
        /// <param name="awayTeamABV">abreviatura de l'equip visitant. Ha d'existir</param>
        /// <param name="awayTeamName">nom de l'equip visitant./param>
        /// <param name="data">data del partit en format dd/mm/aaaa</param>
        /// <returns>retorna un string en format DATA: 21/12/2023 PARTIT : Manchester City 2 - 1 Liverpool
        /// Si no es troba, retornem null </returns>
        public static string GetMatch(string fileMatches, string homeTeamABV, string homeTeamName, string awayTeamABV, string awayTeamName, string data)
        {
            string resultat = null;
            bool trobat = false;
            int gHome, gAway = 0; int goals; string homeFileABV, AwayFileABV;
            StreamReader srTeams = new StreamReader(fileMatches);
            string linia = srTeams.ReadLine();
            while (linia != null && !trobat)
            {
                homeFileABV = srTeams.ReadLine();
                gHome = Convert.ToInt32(srTeams.ReadLine());
                AwayFileABV = srTeams.ReadLine();
                gAway = Convert.ToInt32(srTeams.ReadLine());
                if (linia == data && homeTeamABV == homeFileABV && awayTeamABV == AwayFileABV)
                {
                    resultat = homeTeamName + " " + gHome + "-" + gAway + " " + awayTeamName;
                    trobat = true;
                }
                else
                    linia = srTeams.ReadLine();  //AGAFEM LA SEGÜENT DATA
            }
            srTeams.Close();
            return resultat;
        }
        /// <summary>
        /// valida una data a partir del dia, mes i any donats d'acord als criteris següents:
        /// La data ha de pertànyer al rang de dates entre 01/08/2022 i 30/04/2023, ja que és l'únic rang de dates on poden haver-hi partits
        /// i la data ha de ser vàlida
        /// </summary>
        /// <param name="dia">dia del mes</param>
        /// <param name="mes">mes entre 1 i 12</param>
        /// <param name="any">any entre 2022 i 2023</param>
        /// <returns>true si la data és vàlida</returns>
        public static bool ValidDate(int dia, int mes, int any)
        {
            bool esValida = true;
            if (any != 2023 && any != 2022) esValida = false;
            else if (mes > 12 || mes < 1) esValida = false;
            else
            {
                switch (mes)
                {
                    case 2: if (dia < 1 || dia > 28) esValida = false; break;
                    case 4: case 6: case 9: case 11: if (dia < 1 || dia > 30) esValida = false; break;
                    default: if (dia < 1 || dia > 31) esValida = false; break;
                }
            }
            return esValida;
        }
        /// <summary>
        /// Demana abreviatura de l'equip i mostra els punts obtinguts per l'equip durant tota la temporada
        /// Si l'equip no es troba en el fitxer fileTeams, informem de l'error
        /// </summary>
        /// <param name="fileTeams"></param>
        /// <param name="fileMatches"></param>
        public static void DoGetPointsTeam(string fileTeams, string fileMatches)
        {
            string abv, nomEquip; int nPoints;
            Console.Clear();
            Console.Write("ABREVIATURA DE L'EQUIP-->");
            abv = Console.ReadLine();
            nomEquip = GetTeam(fileTeams, abv);
            if (nomEquip == null) Console.WriteLine("NO EXISTEIX CAP EQUIP AMB AQUESTA ABREVIATURA");
            else
            {
                nPoints = GetPointsTeam(fileMatches, abv);
                Console.WriteLine($"{nomEquip} HA FET {nPoints} DURANT LA TEMPORADA");
            }
            MsgNextScreen("PREM UNA TECLA PER TORNAR AL MENÚ PRINCIPAL");
        }
        /// <summary>
        /// Retorna els punts fets per l'equip amb l'abreviatura 
        /// durant tota la temporada. Si l'equip guanya, obté 3 punts i si empata n'obté 1
        /// </summary>
        /// <param name="fileTeams"></param>
        /// <param name="abreviatura"></param>
        /// <returns></returns>
        public static int GetPointsTeam(string fileMatches, string abreviatura)
        {
            string resultat = null;
            int gHome, gAway;
            int points = 0;
            string homeFileABV, AwayFileABV;
            StreamReader srTeams = new StreamReader(fileMatches);
            string linia = srTeams.ReadLine();
            while (linia != null)
            {
                homeFileABV = srTeams.ReadLine();
                gHome = Convert.ToInt32(srTeams.ReadLine());
                AwayFileABV = srTeams.ReadLine();
                gAway = Convert.ToInt32(srTeams.ReadLine());
                if (abreviatura == homeFileABV || abreviatura == AwayFileABV)
                {
                    points += GetPointsOfMatch(abreviatura, homeFileABV, gHome, gAway);
                }
                linia = srTeams.ReadLine();  //AGAFEM LA SEGÜENT DATA
            }
            srTeams.Close();
            return points;
        }
        /// <summary>
        /// abreviatura coincideix amb l'equip local (homeFileAbv) o bé és visitant. 
        /// Volem saber els punts obtinguts per l'equip "abreviatura". Tant "abreviatura" com "homeFileAbv" han
        /// de ser abreviatures d'equips vàlides.
        /// </summary>
        /// <param name="abreviatura">abreviatura de l'equip del qual volem saber els punts en un partit</param>
        /// <param name="homeFileAbv">abreviatura del nom de l'equip que juga com a local. Pot coincidir amb "abreviatura"
        /// o pot no coincidir (en aquest cas, l'equip "abreviatura" jugaria com a visitant</param>
        /// <param name="gHome">gols fets per l'equip local</param>
        /// <param name="gAway">gols fets per l'equip visitant</param>
        /// <returns></returns>
        public static int GetPointsOfMatch(string abreviatura, string homeFileAbv, int gHome, int gAway)
        {
            int points = 0;
            if (gHome == gAway) points = 1; //EN CAS D'EMPAT TAN SE VAL SI JUGA COM A LOCAR O VISITANT
            else if (abreviatura == homeFileAbv)  //JUGA COM A LOCAL
            {
                if (gHome > gAway) points = 3;
            }
            else
            {
                if (gHome < gAway) points = 3;  //JUGA COM A VISITANT
            }
            return points;
        }
        public static void MostrarMenu()
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    PREMIER LEAGE                    ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                     ║");
            Console.WriteLine("║    1. CERCAR EQUIP                                  ║");
            Console.WriteLine("║    2. GOLS D'UN EQUIP EN UNA TEMPORADA              ║");
            Console.WriteLine("║    3. MOSTRAR RESULTAT D'UN PARTIT CONCRET          ║");
            Console.WriteLine("║    4. PUNTS FETS PER UN EQUIP EN UNA TEMPORADA      ║");
            Console.WriteLine("║    5. PARTITS GUANYATS PER UN EQUIP                 ║");
            Console.WriteLine("║    6. EQUIP AMB MÉS GOLS EN UNA TEMPORADA           ║");
            Console.WriteLine("║    7. EQUIP AMB MÉS GOLS EN UN PARTIT               ║");
            Console.WriteLine("║                                                     ║");
            Console.WriteLine("║    0. EXIT                                          ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════╝");
        }
    }
}
