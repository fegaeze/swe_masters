defmodule BookYourTaxiWeb.BookingControllerTest do
  use BookYourTaxiWeb.ConnCase

  test "user sees error message if distance is negative", %{conn: conn} do
    conn = post conn, "/bookings", %{booking: [pickup_address: "Liivi 2", dropoff_address: "Narva mnt 25", distance: -2]}
    assert html_response(conn, 200) =~ ~r/The distance must not be negative/
  end

  test "user sees error message if dropoff and pickup address is same", %{conn: conn} do
    conn = post conn, "/bookings", %{booking: [pickup_address: "Liivi 2", dropoff_address: "Liivi 2", distance: 2]}
    assert html_response(conn, 200) =~ ~r/Dropoff and Pickup Address cannot be the same/
  end

  test "user sees confirmation message if booking is successful", %{conn: conn} do
    conn = post conn, "/bookings", %{booking: [pickup_address: "Liivi 2", dropoff_address: "Muuseumi tee 2", distance: 2]}
    conn = get conn, redirected_to(conn)
    assert html_response(conn, 200) =~ ~r/A taxi is on your way now/
  end
end
