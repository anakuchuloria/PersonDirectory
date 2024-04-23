using Task.DTO;

namespace Task.Api.Contracts;

public record UserResponse(
    int Id,
    string FirsName,
    String LastName,
    Gender Gender,
    DateTime BirthDate,
    string IdNumber);

