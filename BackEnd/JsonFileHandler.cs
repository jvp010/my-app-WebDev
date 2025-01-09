using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonFileHandler {

    public static List<T> ReadJsonFile<T>(string path){
        if (!File.Exists(path)){
            return default;
        }

        string contents = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(contents);
    }

    public static void WriteToJsonFile<T>(string path, List<T> contents){
        string contents_s = JsonSerializer.Serialize(contents, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, contents_s);
    }
}