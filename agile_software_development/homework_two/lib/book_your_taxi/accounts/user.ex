defmodule BookYourTaxi.Accounts.User do
  use Ecto.Schema
  import Ecto.Changeset

  schema "users" do
    field :age, :integer
    field :email, :string
    field :full_name, :string
    field :password, :string
    field :role, :string
    has_one :taxi, BookYourTaxi.Sales.Taxi
    has_many :bookings, BookYourTaxi.Sales.Booking

    timestamps()
  end

  @doc false
  def changeset(user, attrs) do
    user
    |> cast(attrs, [:full_name, :age, :email, :password, :role])
    |> validate_required([:full_name, :age, :email, :password, :role])
    |> validate_format(:email, ~r/@/)
    |> validate_number(:age, greater_than_or_equal_to: 18)
    |> unique_constraint(:email)
  end
end
