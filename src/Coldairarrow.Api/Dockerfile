FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY . .
EXPOSE 5000
ENTRYPOINT ["dotnet","Coldairarrow.Api.dll"]
