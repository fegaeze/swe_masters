defmodule BookYourTaxi.Repo.Migrations.AddUserAndStatusToTaxi do
  use Ecto.Migration

  def change do
    alter table(:taxis) do
      modify :status, :string, default: "available"
      add :user_id, references(:users)
    end
  end
end
