using System.Text.RegularExpressions;
namespace MoogleEngine;

public class GetFile
{
   //Aqui se obtiene una direccion de la carpeta donde estan los documentos y se crea una lista con todos los paths de los Documentos
   
    readonly static string carpeta = @"C:\Users\Ernesto\Desktop\moogle-main\Content";// Define la ruta de la carpeta donde se encuentran los documentos
    public static List<DataBase> Documentos = new List<DataBase>(); // Crea una lista de objetos Documents
    static public List<string> paths = Directory.GetFiles(carpeta, "*.txt").ToList();//Aqui se va almacenando las palabras 
    static public int CantidadDoc = paths.Count;// Obtiene la cantidad de Documentos en la lista paths"
   
   
   
    public static List<string> PALABRAS = new List<string>();//crea una lista de palabras clave
    public static int TOTALpalabras;
    public GetFile()// Define constructor de la clase
    {
     foreach (string path in paths)
        {
            string Auxiliar = File.ReadAllText(path);//lee el contenido del archivo y lo almacena en una variable
           
            string[] palabras = Regex.Split(Auxiliar.ToLower(), @"\W+");// Aqui se divide el contenido en palabras clave y las almacena en un arreglo de strings 

            Documentos.Add(new DataBase(path, Path.GetFileNameWithoutExtension(path), Auxiliar , palabras , new List<double>(), new List<double>(),"", 0));
            PALABRAS.AddRange(palabras);
        } 
      PALABRAS = PALABRAS.Distinct().ToList();// elimina las palabras clave duplicadas de la lista PALABRAS

      //obtiene el total de esas palabras en la lista anterior
      TOTALpalabras = PALABRAS.Count;
    }
   
   // A continuacion esta clase se llama Extra porque ... esta explicado en el informe
    public static void Extra()
    {
        foreach (DataBase doc in Documentos)
        { 
            doc.TF.Clear();
            doc.TFIDF.Clear();
            //1) Esto sirve para borrar la lista de frecuencias de terminos del objeto Documents
           //2) Lo 2do borra la lista inversa de documentos del objeto Documents

            Busqueda.TF_Query = Enumerable.Repeat(0, GetFile.TOTALpalabras).ToList();// Enumerable es un metodo que crea una secuencia que contiene un elemento repetido varias veces ...
            Busqueda.Docs_Palabras = Enumerable.Repeat(0, GetFile.TOTALpalabras).ToList();
            Busqueda.Sugerencia.Clear();
            Suggestion.Suggest = "";
        }
    }
}



  /*for(int i = 0; i < content.Length; i++)
            {
              string text = File.ReadAllText(content[i]);
              string[] textSplit = text.Split(" ");
              
             for(int j = 0; j < textSplit.Length; j++)
                 {
                    if(query.ToLower() == textSplit[j].ToLower())
                        {
                           SearchItem[] item = new SearchItem[]
                           {
                             new SearchItem( content[i].Substring(30, content[i].Length - 34), MoogleEngine.Snippet(text), 0.5f),
                             new SearchItem( content[i].Substring(30, content[i].Length - 34), MoogleEngine.Snippet(text), 0.3f),
                             new SearchItem( content[i].Substring(30, content[i].Length - 34), MoogleEngine.Snippet(text), 0.2f),
                           };
                           return new SearchResult(item, query);
                        }
                 }  
            }
            SearchItem[] items = new SearchItem[]
                {
                    new SearchItem("Hola", "No hay resultados para su busqueda", 0.5f)
                };
                return new SearchResult( items , query);

    }
}*/

/*public class MoogleEngine 

{
    public static int TF( string query , string[] fileWords)
    {
        int count = 0;
        for(int i = 0; i < fileWords.Length; i++)
        {
            if( query.ToLower() == fileWords[i].ToLower())
            {
                count ++;
            }
        }
        return count;
    }


    public static string Snippet(string text)
    {
        string[] txt = text.Split(' ');
        int txtlength = txt.Length/2;
        string[] snippet = new string[txtlength];
        for(int i = 0; i < txtlength; i++)
        {
            snippet[i] = txt[i];
        }
        string snippetFinal = String.Join(' ', snippet);
        return snippetFinal;
    }

}
}*/
