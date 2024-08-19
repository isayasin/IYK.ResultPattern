using System.Net;
using System.Text.Json.Serialization;

namespace ResultPattern.ClassLibrary;
public sealed class Result<T>
{
    // T tipiyle (genel bir tip) veriyi tutacak property
    public T? Data { get; set; }

    // Hata mesajlarını tutacak property, null olabilir
    public List<string>? ErrorMessages { get; set; }

    // İşlemin başarılı olup olmadığını belirten bool tipi property, varsayılan olarak true
    public bool IsSuccessful { get; set; } = true;

    // JSON serileştirmesi sırasında yoksayılacak bir property
    [JsonIgnore]
    // HTTP durum kodunu tutacak property, varsayılan olarak 200 (OK)
    public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

    // Başarılı bir işlem için kullanılan constructor, sadece veriyi alır
    public Result(T data)
    {
        Data = data;
    }

    // Hata durumunda kullanılan constructor, durum kodu ve hata mesajları alır
    public Result(int statusCode, List<string> errorMessages)
    {
        IsSuccessful = false; // Hata olduğunda başarısız olarak ayarlanır
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    // Hata durumunda kullanılan bir diğer constructor, durum kodu ve tek bir hata mesajı alır
    public Result(int statusCode, string errorMessage)
    {
        IsSuccessful = false; // Hata olduğunda başarısız olarak ayarlanır
        StatusCode = statusCode;
        ErrorMessages = new() { errorMessage }; // Tek hata mesajını listeye ekler
    }

    // Başarılı bir işlem sonucu döndüren static metod
    public static Result<T> Succeed(T data)
    {
        return new(data); // Yeni bir Result nesnesi döndürür
    }

    // Hata durumunda döndürülen static metod, durum kodu ve hata mesajları listesi alır
    public static Result<T> Failure(int statusCode, List<string> errorMessages)
    {
        return new(statusCode, errorMessages); // Yeni bir Result nesnesi döndürür
    }

    // Hata durumunda döndürülen static metod, durum kodu ve tek bir hata mesajı alır
    public static Result<T> Failure(int statusCode, string errorMessage)
    {
        return new(statusCode, errorMessage); // Yeni bir Result nesnesi döndürür
    }

    // Hata durumunda döndürülen static metod, varsayılan olarak 500 durum koduyla birlikte tek bir hata mesajı alır
    public static Result<T> Failure(string errorMessage)
    {
        return new(500, errorMessage); // Yeni bir Result nesnesi döndürür
    }

    // Hata durumunda döndürülen static metod, varsayılan olarak 500 durum koduyla birlikte hata mesajları listesi alır
    public static Result<T> Failure(List<string> errorMessages)
    {
        return new(500, errorMessages); // Yeni bir Result nesnesi döndürür
    }
}