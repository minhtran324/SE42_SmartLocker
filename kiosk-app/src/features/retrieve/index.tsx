// UC-K06: Retrieve Luggage — authenticate, check overdue fee, unlock, sensor confirms.
// UC-K07: Pay Overdue Fee at Kiosk — show fee + payment QR before unlocking.
// UC-K08: Retrieve Luggage in Offline Mode — verify a cached signed credential locally.
// UC-K09: Synchronize Offline Events — push buffered events once back online.
// UC-K10: Confirm Manual Retrieval — sensor-failure fallback with photo evidence.
// UC-K11: Request Support — raise a support request when verification/unlock fails.
export default function RetrievePage() {
  return (
    <section>
      <h1>Retrieve Luggage</h1>
      <p>TODO (UC-K06/K07): overdue-fee check + unlock flow.</p>
      <p>TODO (UC-K08/K09): offline credential verification and event replay queue.</p>
      <p>TODO (UC-K10/K11): manual confirmation and support-request fallback.</p>
    </section>
  );
}
