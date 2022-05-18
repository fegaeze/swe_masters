defmodule BookYourTaxi.Sales.Booking do
  use Ecto.Schema
  import Ecto.Changeset

  schema "bookings" do
    field :distance, :float
    field :dropoff_address, :string
    field :pickup_address, :string
    field :status, :string, default: "open"
    belongs_to :user, BookYourTaxi.Accounts.User

    timestamps()
  end

  @doc false
  def changeset(booking, attrs) do
    booking
    |> cast(attrs, [:pickup_address, :dropoff_address, :distance, :status])
    |> validate_required([:pickup_address, :dropoff_address, :distance])
    |> validate_non_negative_distance(:distance)
    |> validate_address_fields_not_same(:dropoff_address)
  end

  def validate_non_negative_distance(changeset, field, options \\ []) do
    validate_change(changeset, field, fn _, distance ->
      case distance > 0 do
        true -> []
        false -> [{field, options[:message] || "The distance must not be negative"}]
      end
    end)
  end

  def validate_address_fields_not_same(changeset, field, options \\ []) do
    validate_change(changeset, field, fn _, dropoff_address ->
      pickup_address = get_field(changeset, :pickup_address)
      case pickup_address != dropoff_address do
        true -> []
        false -> [{field, options[:message] || "Dropoff and Pickup Address cannot be the same"}]
      end
    end)
  end

end
