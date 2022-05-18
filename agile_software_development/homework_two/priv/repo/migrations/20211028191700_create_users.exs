defmodule BookYourTaxi.Repo.Migrations.CreateUsers do
  use Ecto.Migration

  def change do
    create table(:users) do
      add :full_name, :string
      add :age, :integer
      add :email, :string
      add :password, :string
      add :role, :string

      timestamps()
    end
  end
end
