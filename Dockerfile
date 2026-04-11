# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy file project và restore các gói thư viện
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ code và build
COPY . ./
RUN dotnet publish -c Release -o out

# ===== Run stage =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Lệnh chạy ứng dụng - Nghĩa nhớ kiểm tra tên file .dll nhé
ENTRYPOINT ["dotnet", "nhathan_asp.dll"]