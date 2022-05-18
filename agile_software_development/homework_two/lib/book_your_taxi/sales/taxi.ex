defmodule BookYourTaxi.Sales.Taxi do
  use Ecto.Schema
  import Ecto.Changeset

  schema "taxis" do
    field :location, :string
    field :number_of_seats, :integer
    field :rate, :float
    field :status, :string, default: "available"
    belongs_to :user, BookYourTaxi.Accounts.User

    timestamps()
  end

  @doc false
  def changeset(taxi, attrs) do
    taxi
    |> cast(attrs, [:number_of_seats, :location, :status, :rate])
    |> validate_required([:number_of_seats, :location, :rate])
  end
end
