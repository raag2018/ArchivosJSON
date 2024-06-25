using ArchivosJson;
using System.Net.Http.Json;
using System.Text.Json;
/*Persona persona = new Persona { Nombre = "Juan", Edad = 30 };
// Convertir objeto a JSON (Serialización)
string jsonString = JsonSerializer.Serialize(persona);
Console.WriteLine("Objeto serializado a JSON:");
Console.WriteLine(jsonString);
// Convertir JSON a objeto (Deserialización)
Persona persona2 = JsonSerializer.Deserialize<Persona>(jsonString);
Console.WriteLine("Objeto deserializado desde JSON:");
Console.WriteLine($"Nombre: {persona2.Nombre}, Edad: {persona2.Edad}");
*/
using HttpClient client = new(){
    BaseAddress = new Uri("https://jsonplaceholder.typicode.com/")
};

// Read (GET) a user
User? user = await client.GetFromJsonAsync<User>("users/1");
Console.WriteLine("User fetched:");
PrintUser(user);

// Create (POST) a new user
User newUser = new User { Name = "Jane Doe", Username = "janedoe", Email = "jane.doe@example.com" };
HttpResponseMessage postResponse = await client.PostAsJsonAsync("users", newUser);
if (postResponse.IsSuccessStatusCode){
    User? createdUser = await postResponse.Content.ReadFromJsonAsync<User>();
    Console.WriteLine("User created:");
    PrintUser(createdUser);
}else{
    Console.WriteLine($"Error creating user: {postResponse.StatusCode}");
}

// Update (PUT) a user
User updatedUser = new User { Id = 1, Name = "Updated Name", Username = "updatedusername", Email = "updated.email@example.com" };
HttpResponseMessage putResponse = await client.PutAsJsonAsync("users/1", updatedUser);
if (putResponse.IsSuccessStatusCode){
    User? userAfterUpdate = await putResponse.Content.ReadFromJsonAsync<User>();
    Console.WriteLine("User updated:");
    PrintUser(userAfterUpdate);
}else{
    Console.WriteLine($"Error updating user: {putResponse.StatusCode}");
}

// Delete (DELETE) a user
HttpResponseMessage deleteResponse = await client.DeleteAsync("users/1");
if (deleteResponse.IsSuccessStatusCode){
    Console.WriteLine("User deleted successfully.");
}else{
    Console.WriteLine($"Error deleting user: {deleteResponse.StatusCode}");
}

// Get all users (GET)
User[]? users = await client.GetFromJsonAsync<User[]>("users");
if (users != null){
    Console.WriteLine("All users:");
    foreach (var u in users){
        PrintUser(u);
    }
}else{
    Console.WriteLine("Error fetching users.");
}
 

static void PrintUser(User? user){
    if (user != null){
        Console.WriteLine($"Id: {user.Id}");
        Console.WriteLine($"Name: {user.Name}");
        Console.WriteLine($"Username: {user.Username}");
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine();
    }else{
        Console.WriteLine("User is null");
    }
}