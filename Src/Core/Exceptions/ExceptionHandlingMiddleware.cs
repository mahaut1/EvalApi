using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Passer la requête au middleware suivant dans la chaîne
            await _next(context);
        }
        catch (Exception ex)
        {
            // Gérer l'exception et répondre avec une erreur 500
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Définir le statut HTTP à 500
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Définir le type de contenu comme du texte brut
        context.Response.ContentType = "text/plain";

        // Envoyer un message générique au client
        return context.Response.WriteAsync("Une erreur interne est survenue.");
    }
}
