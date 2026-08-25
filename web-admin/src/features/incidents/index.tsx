// UC-A08: incident lifecycle (open -> investigating -> resolved -> closed).
// UC-A09: remote unlock — mandatory reason, audit log write before the command is sent.
// UC-A10: emergency retrieval coordination when a station's ESP32 is offline.
// UC-A12: 72h abandoned-property workflow — approval must be a different user than the requester.
export default function IncidentsPage() {
  return (
    <section>
      <h1>Incidents &amp; Operations</h1>
      <p>TODO (UC-A08): incident list + status transitions.</p>
      <p>TODO (UC-A09): remote unlock dialog requiring a reason before submit.</p>
      <p>TODO (UC-A10): emergency retrieval dispatch panel.</p>
      <p>TODO (UC-A12): abandoned property queue with segregation-of-duties approval (BR-S04).</p>
    </section>
  );
}
