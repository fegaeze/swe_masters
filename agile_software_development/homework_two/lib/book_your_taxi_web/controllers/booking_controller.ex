defmodule BookYourTaxiWeb.BookingController do
  use BookYourTaxiWeb, :controller

  import Ecto.Query
  alias BookYourTaxi.{Repo, Sales.Booking, Sales.Taxi}

  def new(conn, _params) do
    changeset = Booking.changeset(%Booking{}, %{})
    render(conn, "new.html", changeset: changeset)
  end

  def show(conn, %{"id" => id}) do
    booking = Repo.get!(Booking, id)
    render(conn, "show.html", booking: booking)
  end

  def create(conn, %{"booking" => booking}) do
    changeset = Booking.changeset(%Booking{}, booking)

    case Repo.insert(changeset) do
      {:ok, _booking} ->
        query = from t in Taxi, where: t.status == "available", select: t
        available_taxis = Repo.all(query)

        case length(available_taxis) > 0 do
          true -> conn
                  |> put_flash(:info, "A taxi is on your way now!")
                  |> redirect(to: Routes.booking_path(conn, :new))
          _    -> conn
                  |> put_flash(:info, "All taxis are busy. Try again later.")
                  |> redirect(to: Routes.booking_path(conn, :new))
        end
      {:error, changeset} ->
        render(conn, "new.html", changeset: changeset)
    end
  end

  # FULL IMPLEMENTATION/LOGIC for create_booking which has not undergone TDD/BDD but works via UI
  """
  defp transform("distance", value) do
    String.to_float(value)
  end
  defp transform(_, value), do: value

  def create(conn, %{"booking" => booking_params}) do
    user = conn.assigns.current_user

    booking_struct = Ecto.build_assoc(user, :bookings, Enum.map(booking_params, fn({key, value}) -> {String.to_atom(key), transform(key, value)} end))
    changeset = Booking.changeset(booking_struct, %{}) |> Changeset.put_change(:status, "open")

    booking = Repo.insert!(changeset)

    query = from t in Taxi, where: t.status == "available", order_by: [asc: t.rate, asc: t.completed_rides], limit: 1, select: t
    available_taxis = Repo.all(query)

    case length(available_taxis) > 0 do
      true -> taxi = List.first(available_taxis)
              Multi.new
              |> Multi.insert(:allocation, Allocation.changeset(%Allocation{}, %{status: "accepted"}) |> Changeset.put_change(:booking_id, booking.id) |> Changeset.put_change(:taxi_id, taxi.id))
              |> Multi.update(:taxi, Taxi.changeset(taxi, %{}) |> Changeset.put_change(:status, "busy"))
              |> Multi.update(:booking, Booking.changeset(booking, %{}) |> Changeset.put_change(:status, "allocated"))
              |> Repo.transaction

              conn
              |> put_flash(:info, "A taxi is on your way now!")
              |> redirect(to: Routes.booking_path(conn, :show, booking))
      _ -> Booking.changeset(booking, %{}) |> Changeset.put_change(:status, "rejected") |> Repo.update

              conn
              |> put_flash(:info, "All taxis are busy. Try again later.")
              |> redirect(to: Routes.booking_path(conn, :new))
    end
  end
  """

end
