defmodule WhiteBreadContext do
  use WhiteBread.Context
  use Hound.Helpers

  alias BookYourTaxi.{Repo, Accounts.User, Sales.Taxi}

  feature_starting_state fn  ->
    Application.ensure_all_started(:hound)
    %{}
  end
  scenario_starting_state fn _state ->
    Hound.start_session
    Ecto.Adapters.SQL.Sandbox.checkout(Repo)
    Ecto.Adapters.SQL.Sandbox.mode(Repo, {:shared, self()})
    %{}
  end
  scenario_finalize fn _status, _state ->
    Ecto.Adapters.SQL.Sandbox.checkin(Repo)
    Hound.end_session
  end

  # Scenario: Authenticated customers can view booking form
  given_ ~r/^that I am in the login page$/, fn state ->
    navigate_to "/sessions/new"
    {:ok, state}
  end

  and_ ~r/^I have signed up using the following credentials$/, fn state, %{table_data: users} ->
    users
    |> Enum.map(fn user -> User.changeset(%User{}, user) end)
    |> Enum.each(fn changeset -> Repo.insert!(changeset) end)
    {:ok, state}
  end

  and_ ~r/^I enter my username "(?<username>[^"]+)" and password "(?<password>[^"]+)"$/,
  fn state, %{username: username, password: password} ->
    fill_field({:id, "session_email"}, username)
    fill_field({:id, "session_password"}, password)
    {:ok, state |> Map.put(:username, username) |> Map.put(:password, password)}
  end

  and_ ~r/^I click the Log In button$/, fn state ->
    click({:id, "login-button"})
    {:ok, state}
  end

  and_ ~r/^I am logged in and directed to my user page$/, fn state ->
    {:ok, state}
  end

  when_ ~r/^I click the Book A Taxi button$/, fn state ->
    click({:id, "book-a-taxi-button"})
    {:ok, state}
  end

  then_ ~r/^I should be directed to the booking form$/, fn state ->
    assert element?(:id, "pickup-address") && element?(:id, "dropoff-address")
    {:ok, state}
  end

  # Scenario: A taxi is available for booking
  given_ ~r/^the following taxis on duty$/, fn state, %{table_data: taxis} ->
    taxis
    |> Enum.map(fn taxi -> Taxi.changeset(%Taxi{}, taxi) end)
    |> Enum.each(fn changeset -> Repo.insert!(changeset) end)
    {:ok, state}
  end

  and_ ~r/^I want to go from "(?<pickup_address>[^"]+)" to "(?<dropoff_address>[^"]+)" with distance "(?<distance>[^"]+)" km$/,
  fn state, %{pickup_address: pickup_address, dropoff_address: dropoff_address, distance: distance} ->
    {:ok, state |> Map.put(:pickup_address, pickup_address)
                |> Map.put(:dropoff_address, dropoff_address)
                |> Map.put(:distance, distance)}
  end

  and_ ~r/^I am in the login page$/, fn state ->
    navigate_to "/sessions/new"
    {:ok, state}
  end

  and_ ~r/^I input the trip information$/, fn state ->
    fill_field({:id, "pickup-address"}, state[:pickup_address])
    fill_field({:id, "dropoff-address"}, state[:dropoff_address])
    fill_field({:id, "booking-distance"}, state[:distance])
    {:ok, state}
  end

  when_ ~r/^I submit the booking request$/, fn state ->
    click({:id, "submit-booking-button"})
    {:ok, state}
  end

  then_ ~r/^I should receive a confirmation message$/, fn state ->
    assert visible_in_page? ~r/A taxi is on your way now!/
    {:ok, state}
  end

  # Scenario: No taxi is available for booking
  then_ ~r/^I should receive a rejection message$/, fn state ->
    assert visible_in_page? ~r/All taxis are busy. Try again later./
    {:ok, state}
  end
end
