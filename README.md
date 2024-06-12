# Budget Planner API

### SEEU - Software Oriented Architecture Final Project

## Prerequisites

>>NET 8
> 
>>PostgreSQL

## Setup

1. **Clone the repository:**

    ```bash
    git clone https://github.com/{repo_link}
    cd budget-planner-api
    ```

2. **Restore dependencies:**

    ```bash
    dotnet restore
    ```

3. **Update configuration:**

   Create a `appsettings.json` file in the root directory with the following content:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server_name;Database=BudgetPlannerDb;User Id=your_user_id;Password=your_password;"
      },
      "JWT": {
        "Key": "your_secret_key",
        "Issuer": "your_issuer",
        "Audience": "your_audience"
      }
    }
    ```

4. **Apply migrations and create the database:**

    ```bash
    dotnet ef database update
    ```

5. **Run the application:**

    ```bash
    dotnet run
    ```

