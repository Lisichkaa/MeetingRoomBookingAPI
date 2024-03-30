FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MeetingRoomBooking/*.csproj ./MeetingRoomBooking/
RUN dotnet restore

# copy everything else and build app
COPY MeetingRoomBooking/. ./MeetingRoomBooking/
WORKDIR /app/MeetingRoomBooking
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime
WORKDIR /app
COPY --from=build /app/MeetingRoomBooking/out .
EXPOSE 80
CMD ["dotnet", "MeetingRoomBooking.dll"]