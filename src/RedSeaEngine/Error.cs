namespace OceanApocalypseStudios.RedSea;

/// <summary>
/// Represents an error.
/// </summary>
/// <param name="message">The error message.</param>
/// <param name="code">The error code.</param>
/// <param name="severity">The severity of the error.</param>
public readonly struct Error(string? message, int code, Severity severity)
{
	/// <summary>
	/// The error message.
	/// </summary>
	public string Message => message ?? "";

	/// <summary>
	/// The error code.
	/// </summary>
	public int Code => code;

	/// <summary>
	/// The severity of the error.
	/// </summary>
	public Severity Severity => severity;
}
