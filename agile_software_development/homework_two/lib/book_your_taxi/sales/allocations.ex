defmodule BookYourTaxi.Sales.Allocation do
  use Ecto.Schema
  import Ecto.Changeset

  schema "allocations" do
    field :status, :string
    belongs_to :booking, BookYourTaxi.Sales.Booking
    belongs_to :taxi, BookYourTaxi.Sales.Taxi

    timestamps()
  end

  def changeset(struct, params \\ %{}) do
    struct
    |> cast(params, [:status])
    |> validate_required([:status])
  end
end
