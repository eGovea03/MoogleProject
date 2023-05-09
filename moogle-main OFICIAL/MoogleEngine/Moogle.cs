namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query)// SE define un metodo Query que toma una cadena de consulta como parametro y devuelve un objeto SearchResult
     {
        GetFile.Extra();
       //En la linea anterior se llama al metodo Extra para limpiar busquedas anteriores...
        if (query == "")
        {
            // A continuacion Si la cadena de busqueda esta vacia se crea un Objeto Search Result con un solo valor de busqueda lo que indica que debes ingresar otra cosa para buscar
            SearchItem[] items = new SearchItem[1];
            items[0] = new SearchItem("Ingrese un valor para realizar la busqueda", "" , 0);
            return new SearchResult(items, query);
        }
        else
        {
            Busqueda.query = query.ToLower();
            Busqueda busqueda = new Busqueda();
            Suggestion suggestion = new Suggestion();
            if (Busqueda.OrdenDocs.Count == 0)
            {
                SearchItem[] items = new SearchItem[1];
                items[0] = new SearchItem("No coincide la busqueda con ningun documento ", "", 0);//Se devuelve la lista de sugerencias si los elements no estan ordenados

                return new SearchResult(items, Suggestion.Suggest);
            }
            else {
                int MinDocs= Math.Min(Busqueda.OrdenDocs.Count, 5);
                SearchItem[] items = new SearchItem[MinDocs];
                for (int i = 0; i < MinDocs; i++)
                {
                    items[i] = new SearchItem(Busqueda.OrdenDocs[i].Title, Busqueda.OrdenDocs[i].Snippet , Busqueda.OrdenDocs[i].Score);
                    //con su titulo , fragmento de texto y puntuacion correspondiente
                }
                return new SearchResult(items, Suggestion.Suggest);
            }   
        }
    }
}
