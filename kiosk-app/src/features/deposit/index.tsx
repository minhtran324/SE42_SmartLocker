// UC-K05: Deposit Luggage — authenticate, backend assigns a locker, unlock, sensor confirms close.
export default function DepositPage() {
  return (
    <section>
      <h1>Deposit Luggage</h1>
      <p>TODO (UC-K05): after identity verification, call POST /api/bookings/:id/check-in,
        show assigned locker, wait for DOOR_CLOSED confirmation (UC-I04).</p>
    </section>
  );
}
